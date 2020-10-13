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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CafeMashine.Models;
using CafeMashine.ViewModels;

namespace CafeMashine.Controls
{
    /// <summary>
    /// Логика взаимодействия для UserReporterGrid.xaml
    /// </summary>
    public partial class UserReporterGrid : UserControl
    {
        ReportViewModel viewModel=new ReportViewModel();
        private User _user;
        public UserReporterGrid(User user)
        {
            InitializeComponent();
            _user = user;
        }

        public void CreateGrid(DateTime startDate,DateTime endDate)
        {
            BaseGrid.Children.Clear();
        }
    }
}
