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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyMobile;

namespace CafeMashine.View
{
    /// <summary>
    /// Логика взаимодействия для OperatorData.xaml
    /// </summary>
    public partial class OperatorData : UserControl
    {
        public UserInfo UserInfo { get; set; }
        public OperatorData()
        {
            InitializeComponent();
        }

        public void InitializeData()
        {
            List<Ingredient> ingredients=new List<Ingredient>();
            foreach (Ingredient ingredient in DbProxy.Ingredients.OrderBy(c=>c.Value))
            {
                var outI = DbProxy.Records.Where(c => c.IngredientId == ingredient.Id && c.OperatorId == UserInfo.Id)
                    .Sum(c => c.IngredientCount);
                var inIn = DbProxy.Bases.Where(c => c.Ingredient == ingredient.Id && c.UserId == UserInfo.Id)
                    .Sum(c => c.Count);
                ingredients.Add(new Ingredient()
                {
                    Id=ingredient.Id,
                    Value = ingredient.Value,
                    Count = inIn-outI
                }
                );
            }

            IngredientCountDataGrid.ItemsSource = null;


            IngredientCountDataGrid.ItemsSource = ingredients;

        }
    }
}
