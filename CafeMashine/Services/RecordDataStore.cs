using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeMashine.Models;

namespace CafeMashine.Services
{
   public class RecordDataStore:IDataStore<Record>
    {
        public async Task<bool> AddItemAsync(Record item)
        {
            App.DataBase.AddItem(item);
            return await Task.FromResult(true);
        }

        public Task<bool> UpdateItemAsync(Record item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(Record item)
        {
            throw new NotImplementedException();
        }

        public Task<Record> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Record>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(App.DataBase.Records);
        }

        public Task<IEnumerable<Record>> GetSearchResults(string query)
        {
            throw new NotImplementedException();
        }
    }
}
