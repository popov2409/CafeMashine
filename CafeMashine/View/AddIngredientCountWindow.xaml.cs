﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CafeMashine.Controls;
using CafeMashine.Models;
using CafeMashine.ViewModels;

namespace CafeMashine.View
{
    /// <summary>
    /// Логика взаимодействия для AddIngredientCountWindow.xaml
    /// </summary>
    public partial class AddIngredientCountWindow : Window
    {
        
        private StartViewModel viewModel;
        private User user;
        public AddIngredientCountWindow(StartViewModel viewModel, User user=null)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            this.user = user == null ? viewModel.StorageUser : user;
            InitializeStartList();
        }

        private List<IngredientCount> _startList;
        private List<Ingredient> ingredients;
        async void InitializeStartList()
        {
            _startList=new List<IngredientCount>();
            ingredients = (await viewModel.IngredientDataStore.GetItemsAsync(true)).ToList();
            foreach (Ingredient ingredient in ingredients )
            {
                _startList.Add(new IngredientCount()
                {
                    Id = Guid.NewGuid().ToString(),
                    Ingredient = ingredient.Value,
                    Date = RecordDatePicker.SelectedDate.Value.ToShortDateString(),
                    Count = 0
                });
            }

            BaseListView.ItemsSource = _startList;
        }

        private void AddItems_Click(object sender, RoutedEventArgs e)
        {
            foreach (IngredientCount item in BaseListView.Items)
            {
               if(item.Count==0) continue;
               item.User = user.Id;
               item.Ingredient = viewModel.Ingredients.First(c => c.Value.Equals(item.Ingredient)).Id;
               item.Date = RecordDatePicker.SelectedDate.Value.ToShortDateString();
               viewModel.AddIngredientsCount(item);
            }
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DownValue_Click(object sender, RoutedEventArgs e)
        {
        }

        private void UpValue_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
