using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeMashine.Models;

namespace CafeMashine.Services
{
    public class IngredientDataStore:IDataStore<Ingredient>
    {
        public async Task<bool> AddItemAsync(Ingredient item)
        {
            App.DataBase.AddItem(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Ingredient item)
        {
            App.DataBase.UpdateItem(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Ingredient item)
        {
            App.DataBase.RemoveItem(item);
            return await Task.FromResult(true);
        }

        public Task<Ingredient> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Ingredient>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(App.DataBase.Ingredients.OrderBy(c=>c.Rank));
        }

        public Task<IEnumerable<Ingredient>> GetSearchResults(string query)
        {
            throw new NotImplementedException();
        }
    }
}
