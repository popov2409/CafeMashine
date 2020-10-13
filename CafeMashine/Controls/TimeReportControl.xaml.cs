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

namespace CafeMashine.Controls
{
    /// <summary>
    /// Логика взаимодействия для TimeReportControl.xaml
    /// </summary>
    public partial class TimeReportControl : UserControl
    {
        private List<string> mounth = new List<string>
        {
            "Январь", 
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябрь",
            "Декабрь"
        };
        public TimeReportControl()
        {
            InitializeComponent();
            MounthComboBox.ItemsSource = mounth;
            MounthComboBox.SelectedIndex = DateTime.Now.Month-1;
        }

        private void SelectedTypePeriod_Click(object sender, SelectionChangedEventArgs e)
        {
            switch (PeriodComboBox.SelectedIndex)
            {
                case 0:
                {
                    if (PeriodDates==null)
                    {
                        break;
                    }
                    PeriodDates.Visibility = Visibility.Visible;
                    MounthComboBox.Visibility = Visibility.Collapsed;
                    break;
                }
                case 1:
                {
                    PeriodDates.Visibility = Visibility.Collapsed;
                    MounthComboBox.Visibility = Visibility.Visible;
                    break;
                }
                case 2:
                {
                    PeriodDates.Visibility = Visibility.Collapsed;
                    MounthComboBox.Visibility = Visibility.Collapsed;
                    break;
                }
            }
        }
    }
}
