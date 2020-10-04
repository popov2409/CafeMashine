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
    /// Логика взаимодействия для AvtomatWindow.xaml
    /// </summary>
    public partial class AvtomatWindow : Window
    {
        private AvtomatViewModel viewModel;
        public AvtomatWindow()
        {
            InitializeComponent();
            DataContext = viewModel = new AvtomatViewModel();
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (ValueTextBox.Text.Trim().Length < 1)
            {
                MessageBox.Show("Введите значение в текстовое поле!");
                return;
            }

            viewModel.AddItem(ValueTextBox.Text);
            ValueTextBox.SetBinding(TextBox.TextProperty, new Binding("V") {Source = new object()});
            ValueTextBox.Text = ""; 
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            viewModel.RemoveSelectedItem();
        }

        private void RenameItem_Click(object sender, RoutedEventArgs e)
        {
            viewModel.EditMode = true;
            ValueTextBox.SetBinding(TextBox.TextProperty, new Binding("Value") {Source = viewModel.SelectedItem});
        }
    }
}
