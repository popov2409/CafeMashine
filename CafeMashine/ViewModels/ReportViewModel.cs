using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeMashine.Models;
using CafeMashine.Services;

namespace CafeMashine.ViewModels
{
    public class UserReport
    {
        public UserReport()
        {
            Children=new List<UserReport>();
        }

        public string Value
        {
            get;
            set;
        }

        public List<UserReport> Children { get; set; }
    }

    public class ReportViewModel:BaseViewModel
    {
        private List<Ingredient> _ingredients;
        private List<IngredientCount> _ingredientCounts;
        private List<User> _users;
        private List<Avtomat> _avtomats;
        private List<Record> _records;

        public List<UserReport> GetUserReport(User user,DateTime startDate, DateTime endDate)
        {
            List<UserReport> result=new List<UserReport>();
            LoadIngredients();
            LoadUsers();
            LoadRecods();
            LoadAvtomats();
            var needRec = _records
                .Where(c => c.User == user.Id && DateTime.Parse(c.Date) >= startDate &&
                            DateTime.Parse(c.Date) <= endDate).OrderBy(c => DateTime.Parse(c.Date)).ThenBy(c=>c.Ingredient).ToList();
            if (!needRec.Any()) return result;
            List<string> dates = needRec.Select(c => c.Date).Distinct().ToList();
            foreach (string date in dates)
            {
                UserReport dateReport=new UserReport(){Value = date};
                foreach (Ingredient ingredient in _ingredients)
                {
                    UserReport ingredientReport = new UserReport() {Value = ingredient.Value};
                    var recs = needRec.Where(c => c.Date == date && c.Ingredient == ingredient.Id).ToList();
                    if (recs.Count == 0)
                    {
                        ingredientReport.Value += " - 0";
                    }
                    else
                    {
                        ingredientReport.Value += $" - {recs.Sum(c => c.Count)}";
                        foreach (Record rec in recs)
                        {
                            UserReport avtomatReport = new UserReport()
                                {Value = $"{_avtomats.First(c => c.Id == rec.Avtomat).Value} - {rec.Count}"};
                            ingredientReport.Children.Add(avtomatReport);
                        }

                        ingredientReport.Children = ingredientReport.Children.OrderBy(c => c.Value).ToList();
                    }
                    dateReport.Children.Add(ingredientReport);
                }
                result.Add(dateReport);
            }
            return result;
        }

        async void LoadIngredients()
        {
            _ingredients = (await IngredientDataStore.GetItemsAsync(true)).ToList();
        }

        async void LoadIngredientsCount(DateTime startDate,DateTime endDate)
        {
            _ingredientCounts = (await IngredientCountDataStore.GetItemsAsync(true)).Where(c=>DateTime.Parse(c.Date)>=startDate&& DateTime.Parse(c.Date) <= endDate)
                .OrderBy(c => DateTime.Parse(c.Date)).ThenBy(c=>c.User).ToList();
        }

        async void LoadUsers()
        {
            _users = (await UserDataStore.GetItemsAsync(true)).ToList();
        }

        async void LoadRecods()
        {
            _records = (await RecordDataStore.GetItemsAsync(true)).ToList();
        }

        async void LoadAvtomats()
        {
            _avtomats = (await AvtomatDataStore.GetItemsAsync(true)).ToList();
        }

        public List<List<string>> GetStorageReport(DateTime startDate,DateTime endDate)
        {
            LoadIngredients();
            LoadIngredientsCount(startDate,endDate);
            LoadUsers();
            List<List<string>> resultList = new List<List<string>>();
            if (_ingredientCounts.Count == 0) return resultList;
            List<string> headers=new List<string>();
            headers.Add("Дата");
            headers.Add("Получено/Выдано");
            foreach (Ingredient ingredient in _ingredients)
            {
                headers.Add(ingredient.Value);
            }
            resultList.Add(headers);

            string recDateTime = _ingredientCounts[0].Date;
            List<string> str = new List<string>();
            User us = _users.First(c => c.Id == _ingredientCounts[0].User);
            str.Add(recDateTime);
            str.Add(us.Name.Equals("Storage") ? "Получено" : $"Выдано({us.Name})");
            foreach (Ingredient ingredient in _ingredients)
            {
                str.Add("0");
            }
            resultList.Add(str);
            foreach (IngredientCount ingredientCount in _ingredientCounts)
            {
                if (recDateTime == ingredientCount.Date && ingredientCount.User == us.Id)
                {
                    int i = 2;
                    foreach (Ingredient ingredient in _ingredients)
                    {
                        if (ingredient.Id == ingredientCount.Ingredient)
                        {
                            str[i] = (int.Parse(str[i]) + ingredientCount.Count).ToString();
                        }

                        i++;
                    }
                }
                else
                {
                    recDateTime = ingredientCount.Date;
                    str = new List<string>();
                    resultList.Add(str);
                    us = _users.First(c => c.Id == ingredientCount.User);
                    str.Add(recDateTime);
                    str.Add(us.Name.Equals("Storage") ? "Получено" : $"Выдано({us.Name})");
                    foreach (Ingredient ingredient in _ingredients)
                    {
                        if (ingredient.Id == ingredientCount.Ingredient)
                        {
                            str.Add(ingredientCount.Count.ToString());
                        }
                        else
                        {
                            str.Add("0");
                        }
                    }
                }
            }
            return resultList;
        }
    }
}
