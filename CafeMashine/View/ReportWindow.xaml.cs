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
using CafeMashine.Models;
using CafeMashine.ViewModels;

namespace CafeMashine.View
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        private ReportViewModel viewModel;
        public ReportWindow(User user)
        {
            InitializeComponent();
            viewModel=new ReportViewModel();
            this.Title = user.Name.Equals("Storage") ? "Отчет по складу" : $"Отчет по {user.Name}";
            //button1.Background = (Brush)this.Resources["buttonGradientBrush"];
            //TextBlock tb=new TextBlock(){Style = (Style)this.Resources["buttonGradientBrush"] };
        }

        private void CreateReport_Click(object sender, RoutedEventArgs e)
        {
            List<List<string>> report = viewModel.GetStorageReport();
            CreateGrid(report);
        }

        void CreateGrid(List<List<string>> report)
        {
            ReportGrid.Children.Clear();
        }
    }
}
