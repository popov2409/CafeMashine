using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeMashine.Models
{
    /// <summary>
    /// Класс для учета ингредиентов
    /// </summary>
    public class IngredientCount
    {
        public string Id { get; set; }
        /// <summary>
        /// Id Ингредиента
        /// </summary>
        public string Ingredient { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Дата добавления
        /// </summary>
        public string Date { get; set; }
        
        /// <summary>
        /// Id Пользователя
        /// </summary>
        public string User { get; set; }
    }
}
