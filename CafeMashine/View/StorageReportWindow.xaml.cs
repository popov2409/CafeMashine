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
    /// Логика взаимодействия для StorageReportWindow.xaml
    /// </summary>
    public partial class StorageReportWindow : Window
    {
        private StorageReportViewModel viewModel;
        public StorageReportWindow()
        {
            InitializeComponent();
            DataContext = viewModel = new StorageReportViewModel();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                pd.PrintVisual(ReportGrid, "My First Print Job");
            }
        }
    }
}
