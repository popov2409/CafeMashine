using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeMashine.Models
{
    /// <summary>
    /// Класс пользователя (Пользователь Склад - основной)
    /// </summary>
    public class User
    {
        public string Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
    }
}
