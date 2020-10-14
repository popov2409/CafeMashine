﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
            MounthComboBox.SelectedIndex = DateTime.Now.Month - 1;
        }

        
        public DateTime StartDateValue
        {
            get { return (DateTime)this.GetValue(StartDateValueProperty); }
            set { this.SetValue(StartDateValueProperty, value); }
        }


        public static readonly DependencyProperty StartDateValueProperty =
            DependencyProperty.Register("StartDateValue",
                typeof(DateTime),
                typeof(TimeReportControl),
                new PropertyMetadata(DateTime.Now, StartPropertyChanged));

        private static void StartPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            TimeReportControl mainWindow = source as TimeReportControl;
            DateTime? newValue = e.NewValue as DateTime?;
            // Do additional logic
        }

        public DateTime EndDateValue
        {
            get { return (DateTime)this.GetValue(EndDateValueProperty); }
            set { this.SetValue(EndDateValueProperty, value); }
        }


        public static readonly DependencyProperty EndDateValueProperty =
            DependencyProperty.Register("EndDateValue",
                typeof(DateTime),
                typeof(TimeReportControl),
                new PropertyMetadata(DateTime.Now, EndPropertyChanged));


        private static void EndPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            TimeReportControl mainWindow = source as TimeReportControl;
            DateTime? newValue = e.NewValue as DateTime?;
            // Do additional logic
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

        private void StartDate_Changed(object sender, SelectionChangedEventArgs e)
        {
            if(StartDatePicker.SelectedDate==null) return;
            StartDateValue = DateTime.Parse(StartDatePicker.SelectedDate.ToString());
        }

        private void EndDate_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (EndDatePicker.SelectedDate == null) return;
            EndDateValue = DateTime.Parse(EndDatePicker.SelectedDate.ToString());
        }
    }
}
