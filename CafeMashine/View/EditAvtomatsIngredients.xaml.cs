using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MyMobile;

namespace CafeMashine.View
{
    /// <summary>
    /// Логика взаимодействия для EditAvtomatsIngredients.xaml
    /// </summary>
    public partial class EditAvtomatsIngredients : Window
    {

        public EditAvtomatsIngredients()
        {
            InitializeComponent();
            //DbProxy.LoadData();
            AvtomatListView.ItemsSource = DbProxy.Avtomats.OrderBy(c => c.Value);
            IngredientListView.ItemsSource = DbProxy.Ingredients.OrderBy(c => c.Value);

        }

        private void AddAvtomatButton_OnClick(object sender, RoutedEventArgs e)
        {
            

            if (!AvtomatNameTextBox.Text.Any())
            {
                MessageBox.Show("Введите название!");
                return;
            }

            if (avtomatEdit)
            {
                DbProxy.Avtomats.First(c => c.Id == selectedAvtomat.Id).Value = AvtomatNameTextBox.Text;
                avtomatEdit = false;
            }
            else
            {
           
                DbProxy.Avtomats.Add(new Avtomat() {Id = Guid.NewGuid(), Value = AvtomatNameTextBox.Text});
            }
            AvtomatNameTextBox.Text = "";
            AvtomatListView.ItemsSource = DbProxy.Avtomats.OrderBy(c => c.Value);
            DbProxy.SaveAvtomats();
            AddAvtomatButton.Content = "+";
        }

        private void AvtomatNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(avtomatEdit) return;
            AvtomatListView.ItemsSource = DbProxy.Avtomats
                .Where(c => c.Value.ToLower().Contains(AvtomatNameTextBox.Text.ToLower())).OrderBy(c => c.Value);
        }

        private bool avtomatEdit;
        private Avtomat selectedAvtomat;
        private void RenameAvtomat_OnClick(object sender, RoutedEventArgs e)
        {
            avtomatEdit = true;
            selectedAvtomat=AvtomatListView.SelectedItem as Avtomat;
            AvtomatNameTextBox.Text = selectedAvtomat.Value;
            AddAvtomatButton.Content = "v";

        }

        private bool ingredientEdit;
        private Ingredient selectedIngredient;

        private void AddIngredientButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!IngredientNameTextBox.Text.Any())
            {
                MessageBox.Show("Введите название!");
                return;
            }

            if (ingredientEdit)
            {
                DbProxy.Ingredients.First(c => c.Id == selectedIngredient.Id).Value = IngredientNameTextBox.Text;
            }
            else
            {

                DbProxy.Ingredients.Add(new Ingredient() { Id = Guid.NewGuid(), Value = IngredientNameTextBox.Text });
            }
            IngredientNameTextBox.Text = "";
            IngredientListView.ItemsSource = DbProxy.Ingredients.OrderBy(c => c.Value);
            DbProxy.SaveIngredients();
            AddIngredientButton.Content = "+";
        }

        private void IngredientNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ingredientEdit) return;
            IngredientListView.ItemsSource = DbProxy.Ingredients
                .Where(c => c.Value.ToLower().Contains(IngredientNameTextBox.Text.ToLower())).OrderBy(c => c.Value);
        }

        private void RenameIngredient_OnClick(object sender, RoutedEventArgs e)
        {
            ingredientEdit = true;
            selectedIngredient = IngredientListView.SelectedItem as Ingredient;
            IngredientNameTextBox.Text = selectedIngredient.Value;
            AddIngredientButton.Content = "v";
        }

        private void DeleteAvtomat_OnClick(object sender, RoutedEventArgs e)
        {
            DbProxy.Avtomats.Remove(AvtomatListView.SelectedItem as Avtomat);
            AvtomatListView.ItemsSource = DbProxy.Avtomats.OrderBy(c => c.Value);
            DbProxy.SaveAvtomats();
        }

        private void DeleteIngredienr_OnClick(object sender, RoutedEventArgs e)
        {
            DbProxy.Ingredients.Remove(IngredientListView.SelectedItem as Ingredient);
            IngredientListView.ItemsSource = DbProxy.Ingredients.OrderBy(c => c.Value);
            DbProxy.SaveIngredients();
        }
    }
}
