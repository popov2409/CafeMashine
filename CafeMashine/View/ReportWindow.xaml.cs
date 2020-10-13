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
            ReportGrid.Children.Clear();
            viewModel =new ReportViewModel();
            this.Title = user.Name.Equals("Storage") ? "Отчет по складу" : $"Отчет по {user.Name}";
            StartDate.SelectedDate = DateTime.Parse("1.01." + DateTime.Now.Year);
            EndDate.SelectedDate = DateTime.Parse("31.12." + DateTime.Now.Year);
            if (user.Name.Equals("Storage"))
            {
                List<List<string>> report = viewModel.GetStorageReport(StartDate.DisplayDate, EndDate.DisplayDate);
                CreateGrid(report);
            }
            else
            {
                var res = viewModel.GetUserReport(user, StartDate.DisplayDate, EndDate.DisplayDate);
            }
            //button1.Background = (Brush)this.Resources["buttonGradientBrush"];
            //TextBlock tb=new TextBlock(){Style = (Style)this.Resources["IngredientHeaderStyle"] };
        }

        private void CreateReport_Click(object sender, RoutedEventArgs e)
        {
            List<List<string>> report = viewModel.GetStorageReport(StartDate.DisplayDate,EndDate.DisplayDate);
            CreateGrid(report);
        }

        void CreateGrid(List<List<string>> report)
        {
            ReportGrid.Children.Clear();
            if(report.Count==0) return;
            List<string> header = report[0];
            ReportGrid.RowDefinitions.Add(new RowDefinition(){Height = GridLength.Auto});

            int i = 0;
            int j = 0;
            foreach (string s in header)
            {
                ReportGrid.ColumnDefinitions.Add(new ColumnDefinition(){Width = GridLength.Auto});
                TextBlock tb=new TextBlock(){Text = s};
                if (i < 2)
                {
                    tb.Style = (Style) App.Current.Resources["DateNameStyle"];
                }
                else
                {
                    tb.Style = (Style)App.Current.Resources["IngredientHeaderStyle"];
                }
                Grid.SetRow(tb,j);
                Grid.SetColumn(tb,i);
                ReportGrid.Children.Add(tb);
                i++;
            }

            j++;
            for (int k = 1; k < report.Count; k++)
            {
                i = 0;
                ReportGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                foreach (string s in report[k])
                {
                    TextBlock tb = new TextBlock() { Text = s };
                    if (i < 2)
                    {
                        tb.Style = (Style)App.Current.Resources["RowHeaderStyle"];
                    }
                    else
                    {
                        tb.Style = (Style)App.Current.Resources["BaseContentStyle"];
                    }
                    Grid.SetRow(tb, j);
                    Grid.SetColumn(tb, i);
                    ReportGrid.Children.Add(tb);
                    i++;
                }
                j++;
            }
        }
    }
}
