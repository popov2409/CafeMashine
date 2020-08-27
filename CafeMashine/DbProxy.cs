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
        public static IEnumerable<Avtomat> Avtomats { get; set; }
        public static IEnumerable<Record> Records { get; set; }
        public static IEnumerable<Ingredient> Ingredients { get; set; }


        //public void SaveData()
        //{
        //    StreamWriter writer =new StreamWriter("data.xml",false);
        //    XmlSerializer serializer=new XmlSerializer(typeof(DbProxy));
        //    serializer.Serialize(writer,this);
        //}

        //public static void LoadData()
        //{
        //    StreamReader reader=new StreamReader("data.xml");
        //    XmlSerializer serializer =new XmlSerializer(typeof(DbProxy));
            
        //}


        public static void SaveAvtomats()
        {
            StreamWriter writer = new StreamWriter("avtomats.xml", false);
            XmlSerializer serializer = new XmlSerializer(typeof(IEnumerable<Avtomat>));
            serializer.Serialize(writer, Avtomats);
        }

        public static void SaveIngredients()
        {
            StreamWriter writer = new StreamWriter("ingredienst.xml", false);
            XmlSerializer serializer = new XmlSerializer(typeof(IEnumerable<Ingredient>));
            serializer.Serialize(writer, Avtomats);
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
