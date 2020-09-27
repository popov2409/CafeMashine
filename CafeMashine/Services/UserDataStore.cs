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
        public Task<bool> AddItemAsync(User item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(User item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(User item)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetSearchResults(string query)
        {
            throw new NotImplementedException();
        }
    }
}
