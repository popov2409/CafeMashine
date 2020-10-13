using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeMashine.Models;

namespace CafeMashine.ViewModels
{
    //DateTime now = DateTime.Now;
    //DateTime first = new DateTime(now.Year, now.Month, 1);
    //DateTime last = new DateTime(now.Year, now.Month + 1, 1).AddDays(-1);

    public class StorageReportViewModel:BaseViewModel
    {
        private List<User> _users;
        private User _selectedUser;

        public StorageReportViewModel()
        {
            LoadUsers();
        }

        async void LoadUsers()
        {
            _users = (await UserDataStore.GetItemsAsync(true)).Where(c => !c.Name.Equals("Storage")).ToList();
        }

        public List<User> Users => _users;

        public User SelectedUser
        {
            get => _selectedUser;
            set => _selectedUser = value;
        }
    }
}
