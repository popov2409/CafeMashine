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
        public Task<bool> AddItemAsync(Ingredient item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(Ingredient item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(Ingredient item)
        {
            throw new NotImplementedException();
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
