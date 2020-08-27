using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace MyMobile
{
    public class DbProxy
    {
        public static List<Avtomat> Avtomats { get; set; }
        public static List<Record> Records { get; set; }
        public static List<Ingredient> Ingredients { get; set; }


        /// <summary>
        /// Сохранение списка автоматов
        /// </summary>
        public static void SaveAvtomats()
        {
            StreamWriter writer = new StreamWriter("data//avtomats.xml", false);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Avtomat>));
            serializer.Serialize(writer, Avtomats);
        }

        /// <summary>
        /// Загрузка списка автоматов
        /// </summary>
        public static void LoadAvtomats()
        {
            Avtomats=new List<Avtomat>();
            try
            {
                StreamReader reader=new StreamReader("data//avtomats.xml");
                XmlSerializer serializer=new XmlSerializer(typeof(List<Avtomat>));
                Avtomats = (List<Avtomat>)serializer.Deserialize(reader);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Сохранение списка ингредиентов
        /// </summary>
        public static void SaveIngredients()
        {
            StreamWriter writer = new StreamWriter("data//ingredienst.xml", false);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Ingredient>));
            serializer.Serialize(writer, Ingredients);
        }

        /// <summary>
        /// Загрузка списка ингредиентов
        /// </summary>
        public static void LoadIngredients()
        {
            Ingredients = new List<Ingredient>();
            try
            {
                StreamReader reader = new StreamReader("data//ingredienst.xml");
                XmlSerializer serializer = new XmlSerializer(typeof(List<Ingredient>));
                Ingredients = (List<Ingredient>)serializer.Deserialize(reader);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Загрузка данных
        /// </summary>
        public static void LoadData()
        {
            LoadAvtomats();
            LoadIngredients();
        }

    }

    
    public class Avtomat
    {

        public Guid Id { get; set; }

        public string Value { get; set; }
    }

    public class Ingredient
    {
        public Guid Id { get; set; }

        public string Value { get; set; }

        public int Count { get; set; }
    }

    public class Record
    {
        public Record()
        {
            Id=Guid.NewGuid();
        }

        public Guid Id { get; set; }
        /// <summary>
        /// Какой автомат
        /// </summary>
        public Guid AvtomatId { get; set; }

        /// <summary>
        /// Какой ингредиент
        /// </summary>
        public Guid IngredientId { get; set; }

        /// <summary>
        /// Дата установки
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Колличество ингредиентов
        /// </summary>
        public int IngredientCount { get; set; }

        ///// <summary>
        ///// Отправлено в отчет
        ///// </summary>
        //public bool IsSend { get; set; }
    }

}
