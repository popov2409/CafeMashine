using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using MyMobile;
using Newtonsoft.Json;

namespace CafeMashine.View
{
    /// <summary>
    /// Логика взаимодействия для ImportDataView.xaml
    /// </summary>
    public partial class ImportDataView : Window
    {
        public ImportDataView()
        {
            InitializeComponent();
            OperatorListBox.ItemsSource = DbProxy.UsersInfo.OrderBy(c => c.Name);
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
           
            this.Close();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (OperatorListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбран оператор!");
                return;
            }
            UserInfo info=OperatorListBox.SelectedItem as UserInfo;
            if (info.Avtomats.Count == 0)
            {
                MessageBox.Show(
                    "За данным оператором не закреплены автоматы! \n Выполните насройку и повторите операцию!");
                return;
            }

            if (CreateReport(info))
            {

                MessageBox.Show(
                    "Текстовый файл LIST готов! \n Скопируйте его в телефон и выполните обновление списка на телефоне!");
                this.Close();
                Process PrFolder = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                string file = "LIST.txt";
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Normal;
                psi.FileName = "explorer";
                psi.Arguments = @"/n, /select, " + file;
                PrFolder.StartInfo = psi;
                PrFolder.Start();
            }
            else
            {
                MessageBox.Show("Ошибка создания списка!");
            }
        }

        bool CreateReport(UserInfo userInfo)
        {
            SendUser usrSendUser=new SendUser()
            {
                Id=userInfo.Id.ToString(),
                Name = userInfo.Name
            };
            string result = "";
            foreach (Guid guid in userInfo.Avtomats)
            {
                Avtomat a = DbProxy.Avtomats.First(c => c.Id == guid);
                result += $"{a.Id}:{a.Value};";
            }
            result = result.Remove(result.Length - 1) + "#";

            foreach (Ingredient ingredient in DbProxy.Ingredients)
            {
                result += $"{ingredient.Id}:{ingredient.Value};";
            }
            result = result.Remove(result.Length - 1) + "#";

            result += $"{userInfo.Id}:{userInfo.Name}:{userInfo.Password}:{userInfo.RoleName}";
            StreamWriter sw = new StreamWriter("LIST.txt", false);
            sw.WriteLine(JsonConvert.SerializeObject(usrSendUser));
            foreach (SendAvtomat avtomat in DbProxy.SendAvtomats)
            {
                sw.WriteLine(JsonConvert.SerializeObject(avtomat));
            }
            sw.WriteLine("#");
            foreach (SendIngredient ingredient in DbProxy.SendIngredients)
            {
                sw.WriteLine(JsonConvert.SerializeObject(ingredient));
            }
            sw.Flush();
            sw.Close();
            return true;
        }
    }
}
