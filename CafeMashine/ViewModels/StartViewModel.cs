using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CafeMashine.Models;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace CafeMashine.ViewModels
{
    public class StartViewModel:BaseViewModel
    {
        private List<IngredientCount> _ingredientCounts;
        private List<Ingredient> _ingredients;
        private List<Record> _records;
        private List<User> _users;
        //private List<Avtomat> _avtomats;

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
            //_avtomats = (await AvtomatDataStore.GetItemsAsync(true)).ToList();
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
        public List<IngredientCount> UserIngredientCounts
        {
            get
            {
                _userIngredientCounts = new List<IngredientCount>();
                if (_selectedUser == null) return _userIngredientCounts;
                foreach (var ic in _ingredients.Select(ingredient => new IngredientCount()
                {
                    Id = Guid.NewGuid().ToString(),
                    Ingredient = ingredient.Value,
                    Count = _ingredientCounts.Where(c => c.User == _selectedUser.Id && c.Ingredient == ingredient.Id).Sum(c => c.Count) - _records.Where(c => c.Ingredient == ingredient.Id && c.User == _selectedUser.Id).Sum(c => c.Count)
                }))
                {
                    _userIngredientCounts.Add(ic);
                }
                return _userIngredientCounts;
            }
        }

        public User StorageUser => _storageUser;

        public List<Ingredient> Ingredients => _ingredients;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged("UserIngredientCounts");
            }

        }

        public List<User> Users=> _users.Where(c => c.Name != "Storage").ToList();

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

        public void AddIngredientsCount(IngredientCount item)
        {
            _ingredientCounts.Add(item);
            IngredientCountDataStore.AddItemAsync(item);
            UpdateProperties();
        }

        public void ClearAllData()
        {

        }

        

        public void ExecuteLoadReports(string[] files)
        {
            foreach (string file in files)
            {
                StreamReader sr=new StreamReader(file);
                string[] data = sr.ReadLine()?.Split('#').Where(c => c.Length > 2).ToArray();
                foreach (string s in data)
                {
                    Record r = JsonConvert.DeserializeObject<Record>(s);
                    if(_records.Count(c => c.Id==r.Id)>0) continue;
                    _records.Add(r);
                    RecordDataStore.AddItemAsync(r);
                }

                sr.Close();
            }
            OnPropertyChanged("UserIngredientCounts");
        }

        public void UpdateProperties()
        {
            OnPropertyChanged("IngredientCounts");
            OnPropertyChanged("UserIngredientCounts");
        }

    }
}
