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
    /// Логика взаимодействия для StepIngredientsWindow.xaml
    /// </summary>
    public partial class StepIngredientsWindow : Window
    {
        private bool IsIn { get; set; }
        public StepIngredientsWindow(bool isIn)
        {
            InitializeComponent();
            IsIn = isIn;
            //IngredientListBox.ItemsSource = DbProxy.Ingredients.OrderBy(c => c.Value);
            IngredientGrid.ItemsSource = DbProxy.Ingredients.OrderBy(c => c.Value).ToList();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (Ingredient ingredient in DbProxy.Ingredients)
            {
                DbProxy.Bases.Add(new Base()
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now.ToShortDateString(),
                    Ingredient = ingredient.Id,
                    Count = ingredient.Count,
                    IsIn = IsIn
                });
                ingredient.Count = 0;
            }
            DbProxy.SaveBases();
            this.Close();
        }
    }
}
