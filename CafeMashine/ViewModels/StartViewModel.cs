using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeMashine.Models;

namespace CafeMashine.ViewModels
{
    public class StartViewModel:BaseViewModel
    {
        private List<IngredientCount> _ingredientCounts;
        private List<Ingredient> _ingredients;
        private List<Record> _records;
        private List<User> _users;
        private List<Avtomat> _avtomats;

        private User _storageUser;
        private User _selectedUser;


        private List<IngredientCount> _userIngredientCounts;

        public StartViewModel()
        {
            LoadData();
        }

        private async void LoadData()
        {
            _ingredientCounts = (await IngredientCountDataStore.GetItemsAsync(true)).ToList();
            _avtomats = (await AvtomatDataStore.GetItemsAsync(true)).ToList();
            _ingredients = (await IngredientDataStore.GetItemsAsync(true)).ToList();
            _records = (await RecordDataStore.GetItemsAsync(true)).ToList();
            _users = (await UserDataStore.GetItemsAsync(true)).ToList();

            var strgusr = _users.FirstOrDefault(c => c.Name.Equals("Storage"));
            if (strgusr == null)
            {
                _storageUser = new User() {Id = Guid.NewGuid().ToString(), Name = "Storage"};
                await UserDataStore.AddItemAsync(_storageUser);
            }
            else
            {
                _storageUser = strgusr;
            }
        }

        /// <summary>
        /// Список при выборе оператора
        /// </summary>
        public List<IngredientCount> UserIngredientCounts => _userIngredientCounts;

        public User SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
                _userIngredientCounts = new List<IngredientCount>();
                foreach (var ic in _ingredients.Select(ingredient => new IngredientCount()
                {
                    Id = Guid.NewGuid().ToString(),
                    Ingredient = ingredient.Value,
                    Count = _ingredientCounts.Where(c => c.User == _selectedUser.Id).Sum(c => c.Count) - _records.Where(c => c.Ingredient == ingredient.Id && c.User == _selectedUser.Id).Sum(c => c.Count)
                }))
                {
                    _userIngredientCounts.Add(ic);
                }
                OnPropertyChanged("UserIngredientCounts");
            }

        }


        public List<User> Users
        {
            get
            {
                return _users.Where(c => c.Name != "Storage").ToList();
            }
        }

        public List<IngredientCount> IngredientCounts
        {
            get
            {
                List<IngredientCount> result=new List<IngredientCount>();
                foreach (Ingredient ingredient in _ingredients)
                {
                    IngredientCount ic=new IngredientCount()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Ingredient = ingredient.Value,
                        Count = _ingredientCounts.Where(c=>c.Ingredient==ingredient.Id&&c.User==_storageUser.Id).Sum(c=>c.Count)-_ingredientCounts.Where(c => c.Ingredient == ingredient.Id && c.User != _storageUser.Id).Sum(c => c.Count)
                    };
                    result.Add(ic);
                }
                return result;
            }
        }
    }
}
