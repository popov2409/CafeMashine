using System;
using System.Collections.Generic;
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

namespace CafeMashine.View
{
    /// <summary>
    /// Логика взаимодействия для BigBaseWindow.xaml
    /// </summary>
    public partial class BigBaseWindow : Window
    {
        private List<Ingredient> BaseIngredients { get; set; }
        public BigBaseWindow()
        {
            InitializeComponent();
            DbProxy.LoadData();
            InitializeBase();
        }

        void InitializeBase()
        {
            BaseIngredients=new List<Ingredient>();
            foreach (Ingredient ingredient in DbProxy.Ingredients)
            {
                Ingredient i=new Ingredient()
                {
                    Id = ingredient.Id,
                    Value = ingredient.Value,
                };
                i.Count = DbProxy.Bases.Where(c => c.Ingredient == ingredient.Id && c.IsIn).Sum(c => c.Count) -
                          DbProxy.Bases.Where(c => c.Ingredient == ingredient.Id && !c.IsIn).Sum(c => c.Count);
                BaseIngredients.Add(i);
            }
            IngredientCountDataGrid.ItemsSource= BaseIngredients.OrderBy(c => c.Value).ToList();
            //IngredientCountLIstBox.ItemsSource = BaseIngredients.OrderBy(c => c.Value);

        }


        private void AddIngredientsInBaseButton_OnClick(object sender, RoutedEventArgs e)
        {
            new StepIngredientsWindow(true).ShowDialog();
            InitializeBase();
        }
    }
}
