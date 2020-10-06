using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeMashine.Models;

namespace CafeMashine.Services
{
    public class UserDataStore:IDataStore<User>
    {
        public async Task<bool> AddItemAsync(User item)
        {
            App.DataBase.AddItem(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(User item)
        {
            App.DataBase.UpdateItem(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(User item)
        {
            App.DataBase.RemoveItem(item);
            return await Task.FromResult(true);
        }

        public async Task<User> GetItemAsync(string id)
        {
            return await Task.FromResult(App.DataBase.Users.First(c => c.Id == id));
        }

        public async Task<IEnumerable<User>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(App.DataBase.Users.OrderBy(c=>c.Name));
        }

        public async Task<IEnumerable<User>> GetSearchResults(string query)
        {
            return await Task.FromResult(App.DataBase.Users.Where(c => c.Name.ToLower().Contains(query.ToLower()))
                .OrderBy(c => c.Name));
        }
    }
}
