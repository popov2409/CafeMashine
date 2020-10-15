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
    /// Логика взаимодействия для CreateDataListWindow.xaml
    /// </summary>
    public partial class CreateDataListWindow : Window
    {
        private UserViewModel viewModel;
        public CreateDataListWindow()
        {
            InitializeComponent();
            DataContext = viewModel = new UserViewModel();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateList_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedUser == null)
            {
                MessageBox.Show("Не выбран оператор!");
                return;
            }
            viewModel.CreateUserDataFile();
        }
    }
}
