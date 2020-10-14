using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeMashine.Models;

namespace CafeMashine.ViewModels
{
    //DateTime now = DateTime.Now;
    //DateTime first = new DateTime(now.Year, now.Month, 1);
    //DateTime last = new DateTime(now.Year, now.Month + 1, 1).AddDays(-1);

    public class StorageReportViewModel:BaseViewModel
    {
        private List<User> _users;
        private List<IngredientCount> _ingredientCounts;
        private List<Ingredient> _ingredients;
        private User _selectedUser;
        private DateTime _startDate;
        private DateTime _endDate;


        public StorageReportViewModel()
        {
            LoadList();
        }

        async void LoadList()
        {
            _users = (await UserDataStore.GetItemsAsync(true)).Where(c => !c.Name.Equals("Storage")).ToList();
            _ingredientCounts = (await IngredientCountDataStore.GetItemsAsync(true))
                .OrderBy(c => DateTime.Parse(c.Date)).ToList();
            _ingredients = (await IngredientDataStore.GetItemsAsync(true)).ToList();
        }

        public List<User> Users => _users;

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value; 
                OnPropertyChanged("Report");
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value; 
                OnPropertyChanged("Report");
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged("Report");
            }
        }


        public Object[] Report
        {
            get
            {
                Object[] result = new object[2];
                if(_selectedUser==null||_startDate==null||_endDate==null) {return result;}
                else
                {
                    var recs = _ingredientCounts.Where(c =>
                        c.User == _selectedUser.Id && DateTime.Parse(c.Date) >= _startDate &&
                        DateTime.Parse(c.Date) <= _startDate).ToList();
                    if(recs.Count==0) {return result;}
                    else
                    {
                        List<string> dates = recs.Select(c => c.Date).ToList();
                        result[0] = dates;
                        List<StorageReportStruct> structs=new List<StorageReportStruct>();
                        foreach (Ingredient ingredient in _ingredients)
                        {
                            StorageReportStruct st = new StorageReportStruct()
                                {Ingredient = ingredient.Value, Values = new List<int>()};
                            foreach (string date in dates)
                            {
                                st.Values.Add(recs.Where(c => c.Date == date && c.Ingredient == ingredient.Id)
                                    .Sum(c => c.Count));
                            }
                            structs.Add(st);
                        }

                        result[1] = structs;
                        return result;
                    }
                }
            }
        }
    }

    public struct StorageReportStruct
    {
        public string Ingredient { get; set; }
        public List<int> Values { get; set; }

    }
}
