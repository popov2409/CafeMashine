using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeMashine.Models;

namespace CafeMashine.ViewModels
{
    public class IngredientViewModel:BaseViewModel
    {
        private List<Ingredient> _ingredients;
        private Ingredient _selectedIngredient;

        public List<Ingredient> Ingredients
        {
            get
            {
                LoadList();
                return _ingredients;
            }
        }

        public Ingredient SelectedItem
        {
            get => _selectedIngredient;
            set => _selectedIngredient = value;
        }

        public void DownRank()
        {
            if(SelectedItem==null) return;
            if(SelectedItem.Rank==_ingredients.Count-1) return;
            Ingredient ing=_ingredients.First(c => c.Rank == SelectedItem.Rank + 1);
            ing.Rank--;
            SelectedItem.Rank++;
            IngredientDataStore.UpdateItemAsync(ing);
            IngredientDataStore.UpdateItemAsync(SelectedItem);
            OnPropertyChanged("Ingredients");
        }

        public void UpRank()
        {
            if (SelectedItem == null) return;
            if (SelectedItem.Rank == 0) return;
            Ingredient ing = _ingredients.First(c => c.Rank == SelectedItem.Rank - 1);
            ing.Rank++;
            SelectedItem.Rank--;
            IngredientDataStore.UpdateItemAsync(ing);
            IngredientDataStore.UpdateItemAsync(SelectedItem);
            OnPropertyChanged("Ingredients");
        }

        public async void LoadList()
        {
            _ingredients=new List<Ingredient>();
            _ingredients = (await IngredientDataStore.GetItemsAsync(true)).ToList();
        }

        public void RemoveItem()
        {
            if (SelectedItem == null) return;
            int i = SelectedItem.Rank;
            foreach (Ingredient ingredient in _ingredients.Where(c=>c.Rank>i))
            {
                ingredient.Rank = i;
                IngredientDataStore.UpdateItemAsync(ingredient);
                i++;
            }
            _ingredients.Remove(SelectedItem);
            IngredientDataStore.DeleteItemAsync(SelectedItem);
            OnPropertyChanged("Ingredients");
        }
    }



}
