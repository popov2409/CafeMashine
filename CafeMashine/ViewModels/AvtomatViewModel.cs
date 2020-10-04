using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CafeMashine.Models;
using CafeMashine.Services;

namespace CafeMashine.ViewModels
{
    public class AvtomatViewModel:BaseViewModel
    {
        private List<Avtomat> _avtomats;
        private Avtomat _selectedAvtomat;

        public bool EditMode;

        public List<Avtomat> Avtomats
        {
            get
            {
                LoadList();
                return _avtomats;
            }
        }

        public async void RemoveSelectedItem()
        {
            if(SelectedItem==null) return;
            await AvtomatDataStore.DeleteItemAsync(SelectedItem);
            OnPropertyChanged("Avtomats");
        }

        public Avtomat SelectedItem
        {
            get => _selectedAvtomat;
            set => _selectedAvtomat = value;
        }

        private async void LoadList()
        {
            _avtomats = (await AvtomatDataStore.GetItemsAsync(true)).ToList();
        }

        public void AddItem(string value)
        {
            if (!EditMode)
            {
                Avtomat at = new Avtomat {Id = Guid.NewGuid().ToString(), Value = value};
                AvtomatDataStore.AddItemAsync(at);
            }
            else
            {
                AvtomatDataStore.UpdateItemAsync(SelectedItem);
                EditMode = false;
            }

            OnPropertyChanged("Avtomats");
        }
    }
}
