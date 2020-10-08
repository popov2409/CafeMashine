using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using CafeMashine.Annotations;

namespace CafeMashine.Controls
{
    /// <summary>
    /// Логика взаимодействия для Stepper.xaml
    /// </summary>
    public partial class Stepper : UserControl
    {
        public Stepper()
        {
            InitializeComponent();
            ValueTextBlock.Text = Value.ToString();
            //DataContext = viewModel = new StepperViewModel();
        }

        private void UpValue_Click(object sender, RoutedEventArgs e)
        {
            //viewModel.Value++;
            Value++;
            ValueTextBlock.Text = Value.ToString();
        }

        private void DownValue_Click(object sender, RoutedEventArgs e)
        {
            if (Value == 0) return;
            Value--;
            ValueTextBlock.Text = Value.ToString();
            //if(viewModel.Value==0) return;
            //viewModel.Value--;
        }


        public int Value
        {
            get { return (int) this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value",
                typeof(int),
                typeof(Stepper),
                new PropertyMetadata(0, TestPropertyChanged));

        private static void TestPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            Stepper mainWindow = source as Stepper;
            int? newValue = e.NewValue as int?;
            // Do additional logic
        }

    }
}
