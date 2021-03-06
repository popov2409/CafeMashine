﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private void OpenAvtomatWindow_CLick(object sender, RoutedEventArgs e)
        {
            new AvtomatWindow().ShowDialog();
        }

        private void OpenIngredientWindow_CLick(object sender, RoutedEventArgs e)
        {
            var result= new IngredientWindow().ShowDialog();
            if (result==true)
            {
                DataContext = viewModel = new StartViewModel();
            }
        }

        private void OpenUserWindow_CLick(object sender, RoutedEventArgs e)
        {
            var result = new UserWindow().ShowDialog();
            if (result == true)
            {
                DataContext = viewModel = new StartViewModel();
            }
        }

        private void ReportStorage_Click(object sender, RoutedEventArgs e)
        {
            new StorageReportWindow(0){ Title = (sender as MenuItem).Header.ToString() }.ShowDialog();
        }

        private void ReportUser_Click(object sender, RoutedEventArgs e)
        {
            new StorageReportWindow(2){Title = (sender as MenuItem).Header.ToString() }.ShowDialog();
        }

        private void SelectUserClick(object sender, SelectionChangedEventArgs e)
        {
            ReportViewModel report = new ReportViewModel();
            List<UserReport> res = report.GetUserReport(viewModel.SelectedUser,
                DateTime.Parse($"1.{DateTime.Now.Month}.{DateTime.Now.Year}"),
                DateTime.Parse(
                        $"{(DateTime.Now.Month == 12 ? "31." : "1.")}.{(DateTime.Now.Month == 12 ? "12" : (DateTime.Now.Month + 1).ToString())}.{DateTime.Now.Year}")
                    .AddDays((DateTime.Now.Month == 12 ? 0 : -1)));
            treeView1.ItemsSource = res;
        }

        private void ReportUserAvtomat_Click(object sender, RoutedEventArgs e)
        {
            new StorageReportWindow(1){Title = (sender as MenuItem).Header.ToString()}.ShowDialog();
        }

        private void ReportAvtomat_Click(object sender, RoutedEventArgs e)
        {
            new StorageReportWindow(3) { Title = (sender as MenuItem).Header.ToString() }.ShowDialog();
        }

        private void CreateDataFile_Click(object sender, RoutedEventArgs e)
        {
            new CreateDataListWindow().ShowDialog();
        }
    }
}
