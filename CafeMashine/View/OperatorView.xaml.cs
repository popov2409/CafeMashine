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
    /// Логика взаимодействия для OperatorView.xaml
    /// </summary>
    public partial class OperatorView : Window
    {
        private List<AvtomatsCheck> AvtomatsChecks;

        public OperatorView()
        {
            InitializeComponent();
           // DbProxy.LoadData();
            InitializeList();
        }

        private void InitializeList()
        {
            AvtomatsChecks = new List<AvtomatsCheck>();
            foreach (Avtomat avtomat in DbProxy.Avtomats.OrderBy(c => c.Value))
            {
                AvtomatsChecks.Add(new AvtomatsCheck() {Avtomat = avtomat, IsCheck = false});
            }

            AvtomatListBox.ItemsSource = AvtomatsChecks;

            OperatorListBox.ItemsSource = DbProxy.UsersInfo.OrderBy(c => c.Name);
        }

        private void AddOperatorButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (OperatorNameTextBox.Text.Any())
            {
                DbProxy.UsersInfo.Add(new UserInfo() {Name = OperatorNameTextBox.Text});
            }

            OperatorListBox.ItemsSource = DbProxy.UsersInfo.OrderBy(c => c.Name);
            OperatorNameTextBox.Text = "";
            DbProxy.SaveUsersInfo();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (OperatorListBox.SelectedIndex < 0) return;
            DbProxy.UsersInfo.Remove((UserInfo) OperatorListBox.SelectedItem);
            OperatorListBox.ItemsSource = DbProxy.UsersInfo.OrderBy(c => c.Name);
            DbProxy.SaveUsersInfo();
        }

        private void OperatorListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (AvtomatsCheck avtomatsCheck in AvtomatsChecks)
            {
                avtomatsCheck.IsCheck = false;
            }

            AvtomatListBox.ItemsSource = null;
            AvtomatListBox.ItemsSource = AvtomatsChecks;
            foreach (Guid avtomat in (OperatorListBox.SelectedItem as UserInfo).Avtomats)
            {
                ((List<AvtomatsCheck>) AvtomatListBox.ItemsSource).First(c => c.Avtomat.Id == avtomat).IsCheck = true;
            }
        }

        private void AvtomatIsCheckCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            if (OperatorListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбран оператор!");
                (sender as CheckBox).IsChecked = false;
                return;
            }
            var idA = Guid.Parse((sender as CheckBox).Uid);
            if ((sender as CheckBox).IsChecked == true)
            {
                (OperatorListBox.SelectedItem as UserInfo).Avtomats.Add(idA);

            }
            else
            {
                (OperatorListBox.SelectedItem as UserInfo).Avtomats.Remove(idA);

            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DbProxy.SaveUsersInfo();
        }
    }

    public class AvtomatsCheck
    {
        public Avtomat Avtomat { get; set; }
        public bool IsCheck { get; set; }

    }
}
