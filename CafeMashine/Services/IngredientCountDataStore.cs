using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeMashine.Models;

namespace CafeMashine.Services
{
    public class IngredientCountDataStore:IDataStore<IngredientCount>
    {
        public Task<bool> AddItemAsync(IngredientCount item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(IngredientCount item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(IngredientCount item)
        {
            throw new NotImplementedException();
        }

        public Task<IngredientCount> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IngredientCount>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IngredientCount>> GetSearchResults(string query)
        {
            throw new NotImplementedException();
        }
    }
}
