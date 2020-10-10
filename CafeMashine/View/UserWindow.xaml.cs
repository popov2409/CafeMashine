using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private UserViewModel viewModel;
        private bool? res;
        public UserWindow()
        {
            InitializeComponent();
            DataContext = viewModel = new UserViewModel();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.DialogResult = res;
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (NameUserTextBox.Text.Trim().Length < 1)
            {
                MessageBox.Show("Введите значение в текстовое поле!");
                return;
            }

            viewModel.AddItem(NameUserTextBox.Text);
            NameUserTextBox.SetBinding(TextBox.TextProperty, new Binding("V") { Source = new object() });
            NameUserTextBox.Text = "";
            AddButton.Content = "+";
            res = true;
        }

        private void Checked_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CheckAvtomat(((TextBlock) ((Grid) ((CheckBox) sender).Parent).Children[1]).Text);
        }

        private void Unchecked_Click(object sender, RoutedEventArgs e)
        {
            viewModel.UnCheckAvtomat(((TextBlock)((Grid)((CheckBox)sender).Parent).Children[1]).Text);
        }

        private void RenameUser_Click(object sender, RoutedEventArgs e)
        {
            AddButton.Content = "Ok";
            viewModel.EditMode = true;
            NameUserTextBox.SetBinding(TextBox.TextProperty, new Binding("Name") { Source = viewModel.SelectedUser });
            res = true;
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            viewModel.RemoveUser();
            res = true;
        }

        private void CreateDataFile_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CreateUserDataFile();
        }
    }
}
