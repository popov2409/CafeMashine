using System;
using System.Collections.Generic;
using System.IO;
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
using CafeMashine.Models;
using CafeMashine.ViewModels;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace CafeMashine.View
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartViewModel viewModel;
        public StartWindow()
        {
            InitializeComponent();
            //CreateTestData();
            DataContext = viewModel = new StartViewModel();
        }


        void CreateTestData()
        {
            StreamReader st=new StreamReader("LIST.txt");
            bool isNext = true;
            App.DataBase.AddItem(JsonConvert.DeserializeObject<User>(st.ReadLine()));

            while (!st.EndOfStream)
            {
                string str = st.ReadLine();
                if (str.Equals("#"))
                {
                    isNext = false;
                    continue;
                }
                if (isNext)
                {
                    continue;
                    App.DataBase.AddItem(JsonConvert.DeserializeObject<Avtomat>(str));
                }
                else
                {
                    Ingredient ingredient = JsonConvert.DeserializeObject<Ingredient>(str);
                    ingredient.Rank = 0;
                    App.DataBase.AddItem(ingredient);
                }
            }
        }

        private void AddInStorage_Click(object sender, RoutedEventArgs e)
        {
            new AddIngredientCountWindow(viewModel).ShowDialog();
        }

        private void AddInUser_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedUser == null)
            {
                MessageBox.Show("Не выбран оператор!");
                return;
            }
            new AddIngredientCountWindow(viewModel,viewModel.SelectedUser).ShowDialog();
        }

        private void LoadRaports_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd=new OpenFileDialog(){Multiselect = true};
            if (ofd.ShowDialog() == true)
            {
                viewModel.ExecuteLoadReports(ofd.FileNames);
            }
        }
    }
}
