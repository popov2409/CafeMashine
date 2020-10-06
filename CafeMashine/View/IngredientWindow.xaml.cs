using System;
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
using CafeMashine.ViewModels;

namespace CafeMashine.View
{
    /// <summary>
    /// Логика взаимодействия для IngredientWindow.xaml
    /// </summary>
    public partial class IngredientWindow : Window
    {
        private IngredientViewModel viewModel;
        private bool? res;
        public IngredientWindow()
        {
            InitializeComponent();
            DataContext = viewModel = new IngredientViewModel();
        }

        private void RenameItem_Click(object sender, RoutedEventArgs e)
        {
            AddButton.Content = "Ok";
            viewModel.EditMode = true;
            ValueTextBox.SetBinding(TextBox.TextProperty, new Binding("Value") { Source = viewModel.SelectedItem });
            this.res = true;
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            viewModel.RemoveItem();
            this.res = true;
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (ValueTextBox.Text.Trim().Length < 1)
            {
                MessageBox.Show("Введите значение в текстовое поле!");
                return;
            }

            viewModel.AddItem(ValueTextBox.Text);
            ValueTextBox.SetBinding(TextBox.TextProperty, new Binding("V") { Source = new object() });
            ValueTextBox.Text = "";
            AddButton.Content = "+";
            this.res = true;
        }

        private void UpItem_Click(object sender, RoutedEventArgs e)
        {
            viewModel.UpRank();
            this.res = true;
        }

        private void DownItem_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DownRank();
            this.res = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.DialogResult = this.res;
        }
    }
}
