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

        public AvtomatViewModel()
        {
            LoadList();
        }

        public List<Avtomat> Avtomats => _avtomats;


        public Avtomat SelectedItem
        {
            get => _selectedAvtomat;
            set => _selectedAvtomat = value;
        }

        private async void LoadList()
        {
            _avtomats = (await AvtomatDataStore.GetItemsAsync(true)).ToList();
        }
    }
}
