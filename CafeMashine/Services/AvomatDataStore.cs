using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeMashine.Models;

namespace CafeMashine.Services
{
    public class AvomatDataStore:IDataStore<Avtomat>
    {
        public async Task<bool> AddItemAsync(Avtomat item)
        {
            App.DataBase.AddItem(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Avtomat item)
        {
            App.DataBase.UpdateItem(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Avtomat item)
        {
            App.DataBase.RemoveItem(item);
            return await Task.FromResult(true);
        }

        public async Task<Avtomat> GetItemAsync(string id)
        {
            return await Task.FromResult(App.DataBase.Avtomats.First(c => c.Id == id));
        }

        public async Task<IEnumerable<Avtomat>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(App.DataBase.Avtomats.OrderBy(c => c.Value));
        }

        public async Task<IEnumerable<Avtomat>> GetSearchResults(string query)
        {
            return  await Task.FromResult(App.DataBase.Avtomats.Where(c => c.Value.ToLower().Contains(query)));
        }
    }
}
