using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using CafeMashine.Services;

namespace CafeMashine
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static DataProxy _dataProxy;

        public static DataProxy DataBase => _dataProxy ?? (_dataProxy = new DataProxy());
    }
}
