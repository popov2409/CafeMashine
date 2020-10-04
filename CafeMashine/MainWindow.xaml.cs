using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Dynamic;
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
using CafeMashine.Services;
using CafeMashine.View;
using Microsoft.Win32;
using MyMobile;
using Ingredient = CafeMashine.Models.Ingredient;

namespace CafeMashine
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<OperatorData> operatorDatas;
        public MainWindow()
        {
            InitializeComponent();
            List<Ingredient> ing = App.DataBase.Ingredients;
            int i = 0;
            foreach (Ingredient ingredient in ing)
            {
                ingredient.Rank = i;
                App.DataBase.UpdateItem(ingredient);
                i++;
            }

            //DbProxy.LoadData();
            //InitializeBase();
            //InitializeOperatorPages();
            //TestColumn();
            //DataGridTextColumn col=new DataGridTextColumn(){ Header = "Вася" };
            //var boundItem = IngredientCountDataGrid.CurrentCell.Item;
            //var binding = col.Binding as Binding;
            //var propertyName = binding.Path.Path;
            //var propInfo = boundItem.GetType().GetProperty(propertyName);
            //int[] intt = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            //propInfo.SetValue(boundItem, intt,new object[] {});
            //IngredientCountDataGrid.Columns.Add(col);

        }

        public ObservableCollection<double> DataList { get; set;}

        class MyClass
        {
            public int Value;
        }



        void TestColumn()
        {
            DataGrid dataGrid;

            string[] labels = new string[] { "Column 0", "Column 1", "Column 2" };

            foreach (string label in labels)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = label;
                column.Binding = new Binding(label.Replace(' ', '_'));

                IngredientCountDataGrid.Columns.Add(column);
            }

            int[] values = new int[] { 0, 1, 2 };

            dynamic row = new ExpandoObject();

            for (int j = 0; j < 12; j++)
            {
                for (int i = 0; i < labels.Length; i++)
                    ((IDictionary<String, Object>)row)[labels[i].Replace(' ', '_')] = values[i];

                IngredientCountDataGrid.Items.Add(row);
            }

            




            //List<MyClass> list=new List<MyClass>();
            //for (int i = 0; i < 12; i++)
            //{
            //    list.Add(new MyClass(){Value = i});
            //} 
            //DataList = new ObservableCollection<double>(new List<double>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });
            //int n = 0;
            //DataGridTextColumn col = new DataGridTextColumn();
            //col.Header = "Вася";
            //Binding binding = new Binding("");
            //binding.Source = list;
            //// binding.Mode = BindingMode.TwoWay;
            //col.Binding = binding;

            //IngredientCountDataGrid.Columns.Add(col);
        }


        void InitializeOperatorPages()
        {
            operatorDatas=new List<OperatorData>();
            foreach (UserInfo info in DbProxy.UsersInfo)
            {
                OperatorData op = new OperatorData();
                op.UserInfo = info;
                op.InitializeData();
                operatorDatas.Add(op);
                MainTabControl.Items.Add(new TabItem() { Header = op.UserInfo.Name, Content = op });
            }
            
        }

        void UpdatePages()
        {
            foreach (OperatorData operatorData in operatorDatas)
            {
                operatorData.InitializeData();
            }
        }

        private void EditListMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            new EditAvtomatsIngredients().ShowDialog();
        }

        private void ImportListMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            new ImportDataView().ShowDialog();
        }



        //string data = "THExxQUICKxxBROWNxxFOX";

        //    return data.Split(new string[] { "xx" }, StringSplitOptions.None);
        private void OperatorListMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            new OperatorView().ShowDialog();
        }

        private void LoadReportMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog ofd=new OpenFileDialog(){Multiselect = true};

            if (ofd.ShowDialog() == true)
            {
                List<string> data=new List<string>();
                foreach (string fileName in ofd.FileNames)
                {
                    StreamReader sr=new StreamReader(fileName);
                    data.Add(sr.ReadLine());
                    sr.Close();
                }

                if (DbProxy.ImportReportes(data))
                {
                    MessageBox.Show("Данные загружены!");
                }
                else
                {
                    MessageBox.Show("Ошибка импорта!");
                }
            }
        }

        private void AddIngredientsInBaseButton_OnClick(object sender, RoutedEventArgs e)
        {
            new StepIngredientsWindow(true).ShowDialog();
            InitializeBase();
        }
        private List<Ingredient> BaseIngredients { get; set; }
        void InitializeBase()
        {
            BaseIngredients = new List<Ingredient>();
            //foreach (Ingredient ingredient in DbProxy.Ingredients)
            //{
            //    Ingredient i = new Ingredient()
            //    {
            //        Id = ingredient.Id,
            //        Value = ingredient.Value,
            //    };
            //    i.Count = DbProxy.Bases.Where(c => c.Ingredient == ingredient.Id && c.IsIn).Sum(c => c.Count) -
            //              DbProxy.Bases.Where(c => c.Ingredient == ingredient.Id && !c.IsIn).Sum(c => c.Count);
            //    BaseIngredients.Add(i);
            //}
         //   IngredientCountDataGrid.ItemsSource = BaseIngredients.OrderBy(c => c.Value).ToList();



        }

        private void SendIngredientsInBaseButton_OnClick(object sender, RoutedEventArgs e)
        {
            new StepIngredientsWindow(false).ShowDialog();
            InitializeBase();
            UpdatePages();
        }
    }

}
