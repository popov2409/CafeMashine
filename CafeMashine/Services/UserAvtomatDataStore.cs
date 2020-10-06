using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeMashine.Models;

namespace CafeMashine.Services
{
    public class UserAvtomatDataStore:IDataStore<UserAvtomat>
    {
        public async Task<bool> AddItemAsync(UserAvtomat item)
        {
            App.DataBase.AddItem(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(UserAvtomat item)
        {
            App.DataBase.UpdateItem(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(UserAvtomat item)
        {
            App.DataBase.RemoveItem(item);
            return await Task.FromResult(true);
        }

        public async Task<UserAvtomat> GetItemAsync(string id)
        {
            return await Task.FromResult(App.DataBase.UserAvtomats.First(c => c.Id == id));
        }

        public async Task<IEnumerable<UserAvtomat>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(App.DataBase.UserAvtomats);
        }

        public Task<IEnumerable<UserAvtomat>> GetSearchResults(string query)
        {
            throw new NotImplementedException();
        }
    }
}
