using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CafeMashine.Annotations;
using CafeMashine.Models;
using CafeMashine.Services;

namespace CafeMashine.ViewModels
{
    public class BaseViewModel:INotifyPropertyChanged
    {
        private IDataStore<Avtomat> _avtomatStore;
        private IDataStore<Record> _recordStore;
        private IDataStore<User> _userStore;
        private IDataStore<Ingredient> _ingredientStore;
        private IDataStore<IngredientCount> _ingredienCountStore;
        private IDataStore<UserAvtomat> _useravtomatDataStore;
        public IDataStore<Avtomat> AvtomatDataStore => _avtomatStore ?? new AvomatDataStore();
        public IDataStore<Record> RecordDataStore => _recordStore ?? new RecordDataStore();
        public IDataStore<User> UserDataStore => _userStore ?? new UserDataStore();
        public IDataStore<Ingredient> IngredientDataStore => _ingredientStore ?? new IngredientDataStore();
        public IDataStore<IngredientCount> IngredientCountDataStore =>
            _ingredienCountStore ?? new IngredientCountDataStore();
        public IDataStore<UserAvtomat> UserAvtomatDataStore => _useravtomatDataStore ?? new UserAvtomatDataStore();


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
