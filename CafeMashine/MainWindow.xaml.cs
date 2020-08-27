using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CafeMashine.View;
using MyMobile;

namespace CafeMashine
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DbProxy.LoadData();
        }

        private void EditListMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            new EditAvtomatsIngredients().ShowDialog();
        }

        private void ImportListMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            string result = "";
            foreach (Avtomat avtomat in DbProxy.Avtomats)
            {
                result += $"{avtomat.Id}:{avtomat.Value};";
            }

            result = result.Remove(result.Length - 1);

            result += "#";

            foreach (Ingredient ingredient in DbProxy.Ingredients)
            {
                result += $"{ingredient.Id}:{ingredient.Value};";
            }
            result = result.Remove(result.Length - 1);

            StreamWriter sw = new StreamWriter("LIST.txt", false);
            sw.WriteLine(result);
            sw.Flush();
            sw.Close();
            MessageBox.Show("Текстовый файл LIST готов! \n Скопируйте его в телефон и выполните обновление списка на телефоне!");
        }



        //string data = "THExxQUICKxxBROWNxxFOX";

        //    return data.Split(new string[] { "xx" }, StringSplitOptions.None);
    }
}
