using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeMashine.Models
{
    /// <summary>
    /// Класс данных по внесенным ингредиентам(необходим для обмена данных)
    /// </summary>
    public class Record
    {
        public string Id { get; set; }
        /// <summary>
        /// Id автомата
        /// </summary>
        public string Avtomat { get; set; }
        /// <summary>
        /// Id ингредиента
        /// </summary>
        public string Ingredient { get; set; }
        /// <summary>
        /// Колличество
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Дата
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// Отправлено(используется только на телефоне)
        /// </summary>
        public bool IsSend { get; set; }
        /// <summary>
        /// Заблокировано (используется только на телефоне)
        /// </summary>
        public bool IsBlock { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public string User { get; set; }
    }
}
