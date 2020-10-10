using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CafeMashine.Models;
using CafeMashine.Services;
using Newtonsoft.Json;

namespace CafeMashine.ViewModels
{
    public class UserViewModel:BaseViewModel
    {
        private List<User> _users;
        private List<UserAvtomat> _userAvtomats;
        private List<Avtomat> _avtomats;
        private List<AvtomatCheck> _avtomatChecks;
        private User _selectedUser;

        public bool EditMode;

        public List<User> Users
        {
            get
            {
                LoadUsers();
                LoadLists();
                return _users;
            }
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged("AvtomatChecks");
            }
        }


        public List<AvtomatCheck> AvtomatChecks
        {
            get
            {
                _avtomatChecks=new List<AvtomatCheck>();
                if (_selectedUser == null) return _avtomatChecks;
                List<string> avId = _userAvtomats.Where(c => c.User.Equals(_selectedUser.Id)).Select(c => c.Avtomat)
                    .ToList()??new List<string>();
                foreach (Avtomat avtomat in _avtomats)
                {
                    _avtomatChecks.Add(new AvtomatCheck()
                        {
                            Avtomat = avtomat,
                            IsCheck = avId.Contains(avtomat.Id)
                        }
                    );
                }
                return _avtomatChecks;
            }
        }

        public void CheckAvtomat(string a)
        {
            Avtomat av = _avtomats.First(c => c.Value.Equals(a));
            UserAvtomat ua = new UserAvtomat()
                {Id = Guid.NewGuid().ToString(), Avtomat = av.Id, User = _selectedUser.Id};
            _userAvtomats.Add(ua);
            UserAvtomatDataStore.AddItemAsync(ua);
        }

        public void UnCheckAvtomat(string a)
        {
            Avtomat av = _avtomats.First(c => c.Value.Equals(a));
            UserAvtomat ua = _userAvtomats.First(c => c.Avtomat == av.Id && c.User == _selectedUser.Id);
            _userAvtomats.Remove(ua);
            UserAvtomatDataStore.DeleteItemAsync(ua);
        }

        private async void LoadUsers()
        {
            _users = (await UserDataStore.GetItemsAsync(true)).Where(c => !c.Name.Equals("Storage")).ToList();
        }


        private async void LoadLists()
        {
            _avtomats = (await AvtomatDataStore.GetItemsAsync(true)).ToList();
            _userAvtomats = (await UserAvtomatDataStore.GetItemsAsync(true)).ToList();
        }

        public void AddItem(string name)
        {
            if (!EditMode)
            {
                User us = new User() { Id = Guid.NewGuid().ToString(), Name = name };
                UserDataStore.AddItemAsync(us);
            }
            else
            {
                UserDataStore.UpdateItemAsync(SelectedUser);
                EditMode = false;
            }

            OnPropertyChanged("Users");
        }

        public void RemoveUser()
        {
            if(_selectedUser==null) return;
            var au = _userAvtomats.Where(c => c.User == _selectedUser.Id);
            UserDataStore.DeleteItemAsync(_selectedUser);
            foreach (UserAvtomat userAvtomat in au)
            {
                UserAvtomatDataStore.DeleteItemAsync(userAvtomat);
            }
            OnPropertyChanged("Users");
        }

        public async void CreateUserDataFile()
        {
            if (_selectedUser == null) return;
            var l_avtomats = (await UserAvtomatDataStore.GetItemsAsync(true)).Where(c => c.User == _selectedUser.Id)
                .Select(c => c.Avtomat);
            if (l_avtomats == null || !l_avtomats.Any())
            {
                MessageBox.Show("Для данного оператора не указаны атоматы!");
                return;
            }
            List<Avtomat> uAvtomats = new List<Avtomat>();
            foreach (string s in l_avtomats)
            {
                uAvtomats.Add((await AvtomatDataStore.GetItemsAsync()).First(c => c.Id == s));
            }

            StreamWriter sr = new StreamWriter("LIST.txt", false);
            sr.WriteLine(JsonConvert.SerializeObject(_selectedUser));
            foreach (Avtomat avtomat in uAvtomats)
            {
                sr.WriteLine(JsonConvert.SerializeObject(avtomat));
            }
            sr.WriteLine("#");
            var _ingredients = await IngredientDataStore.GetItemsAsync(true);
            foreach (Ingredient ingredient in _ingredients)
            {
                sr.WriteLine(JsonConvert.SerializeObject(ingredient));
            }
            sr.Close();
            MessageBox.Show("Файл данных LIST.txt готов!");
        }

        public class AvtomatCheck
        {
            public bool IsCheck { get; set; }
            public Avtomat Avtomat { get; set; }
        }
    }

}
