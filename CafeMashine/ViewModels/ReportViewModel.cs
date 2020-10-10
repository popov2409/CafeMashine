using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeMashine.Models;
using CafeMashine.Services;

namespace CafeMashine.ViewModels
{
    public class ReportViewModel:BaseViewModel
    {
        private List<Ingredient> _ingredients;
        private List<IngredientCount> _ingredientCounts;
        private List<User> _users;

        public List<List<string>> GetUserReport(User user)
        {
            List<List<string>> resultList=new List<List<string>>();

            return resultList;
        }

        async void LoadIngredients()
        {
            _ingredients = (await IngredientDataStore.GetItemsAsync(true)).ToList();
        }

        async void LoadIngredientsCount()
        {
            _ingredientCounts = (await IngredientCountDataStore.GetItemsAsync(true))
                .OrderBy(c => DateTime.Parse(c.Date)).ThenBy(c=>c.User).ToList();
        }

        async void LoadUsers()
        {
            _users = (await UserDataStore.GetItemsAsync(true)).ToList();
        }

        public List<List<string>> GetStorageReport()
        {
            LoadIngredients();
            LoadIngredientsCount();
            LoadUsers();
            List<List<string>> resultList = new List<List<string>>();
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
