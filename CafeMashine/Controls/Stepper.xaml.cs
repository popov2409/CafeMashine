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
using System.Windows.Threading;
using CafeMashine.Annotations;

namespace CafeMashine.Controls
{
    /// <summary>
    /// Логика взаимодействия для Stepper.xaml
    /// </summary>
    public partial class Stepper : UserControl
    {
        private DispatcherTimer timer;
        public Stepper()
        {
            InitializeComponent();
            ValueTextBlock.Text = Value.ToString();
            InitializeTimer();
        }

        void InitializeTimer()
        {
            timer=new DispatcherTimer();
            timer.Interval = new TimeSpan(0,0,0,0,100);
            timer.Tick+=TimerOnTick;
        }

        private int step = 0;
        private void TimerOnTick(object sender, EventArgs e)
        {
            pause++;
            if (pause < 2)
            {
                return;
            }
            if(Value==0 && step<0) return;
            Value += pause > 10 ? step * 2 : step;
            ValueTextBlock.Text = Value.ToString();
        }

        private void UpValue_Click(object sender, RoutedEventArgs e)
        {
            Value++;
            ValueTextBlock.Text = Value.ToString();
        }

        private void DownValue_Click(object sender, RoutedEventArgs e)
        {
            //if (Value == 0) return;
            Value--;
            ValueTextBlock.Text = Value.ToString();
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

        private int pause = 0;
        private void UpButton_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            step = 1;
            timer.Start();
        }

        private void UpButton_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            pause = 0;
            step = 0;
            timer.Stop();
        }

        private void DownButton_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            step = -1;
            timer.Start();
        }
    }
}
