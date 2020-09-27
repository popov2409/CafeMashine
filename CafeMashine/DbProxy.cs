using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Newtonsoft.Json;
namespace MyMobile
{
    public class DbProxy
    {
        public static List<Avtomat> Avtomats { get; set; }
        public static List<Record> Records { get; set; }
        public static List<Ingredient> Ingredients { get; set; }

        public static List<Base> Bases { get; set; }

        public static List<UserInfo> UsersInfo { get; set; }

        /// <summary>
        /// Сохранение списка автоматов
        /// </summary>
        public static void SaveAvtomats()
        {
            StreamWriter writer = new StreamWriter("data//avtomats.xml", false);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Avtomat>));
            serializer.Serialize(writer, Avtomats);
            writer.Close();
        }

        /// <summary>
        /// Загрузка списка автоматов
        /// </summary>
        ///
        public static List<SendAvtomat> SendAvtomats;
        private static void LoadAvtomats()
        {
            Avtomats=new List<Avtomat>();
            try
            {
                StreamReader reader=new StreamReader("data//Avtomats.xml");
                XmlSerializer serializer=new XmlSerializer(typeof(List<Avtomat>));
                Avtomats = (List<Avtomat>)serializer.Deserialize(reader);
                reader.Close();
            }
            catch
            {
                // ignored
            }

            SendAvtomats=new List<SendAvtomat>();
            foreach (Avtomat avtomat in Avtomats)
            {
                SendAvtomats.Add(new SendAvtomat(){Id = avtomat.Id.ToString(),Value = avtomat.Value});
            }

        }


        private static void SaveRecords()
        {
            StreamWriter writer = new StreamWriter("data//Records.xml", false);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Record>));
            serializer.Serialize(writer, Records);
            writer.Close();
        }

        private static void LoadRecords()
        {
            Records = new List<Record>();
            try
            {
                StreamReader reader = new StreamReader("data//Records.xml");
                XmlSerializer serializer = new XmlSerializer(typeof(List<Record>));
                Records = (List<Record>)serializer.Deserialize(reader);
                reader.Close();
            }
            catch
            {
                // ignored
            }
        }

        public static void SaveUsersInfo()
        {
            StreamWriter writer = new StreamWriter("data//UsersInfo.xml", false);
            XmlSerializer serializer = new XmlSerializer(typeof(List<UserInfo>));
            serializer.Serialize(writer, UsersInfo);
            writer.Close();
        }

        private static void LoadUsersInfo()
        {
            UsersInfo=new List<UserInfo>();
            try
            {
                StreamReader reader = new StreamReader("data//UsersInfo.xml");
                XmlSerializer serializer = new XmlSerializer(typeof(List<UserInfo>));
                UsersInfo = (List<UserInfo>)serializer.Deserialize(reader);
                reader.Close();
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
            StreamWriter writer = new StreamWriter("data//Ingredients.xml", false);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Ingredient>));
            serializer.Serialize(writer, Ingredients);
            writer.Close();
        }

        public static List<SendIngredient> SendIngredients;
        /// <summary>
        /// Загрузка списка ингредиентов
        /// </summary>
        private static void LoadIngredients()
        {
            Ingredients = new List<Ingredient>();
            try
            {
                StreamReader reader = new StreamReader("data//Ingredients.xml");
                XmlSerializer serializer = new XmlSerializer(typeof(List<Ingredient>));
                Ingredients = (List<Ingredient>)serializer.Deserialize(reader);
                reader.Close();
            }
            catch
            {
                // ignored
            }
            SendIngredients=new List<SendIngredient>();
            foreach (Ingredient ingredient in Ingredients)
            {
                SendIngredients.Add(new SendIngredient() {Id = ingredient.Id.ToString(), Value = ingredient.Value});
            }
        }

        public static void SaveBases()
        {
            StreamWriter writer = new StreamWriter("data//Bases.xml", false);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Base>));
            serializer.Serialize(writer, Bases);
            writer.Close();
        }

        private static void LoadBases()
        {
            Bases=new List<Base>();
            try
            {
                StreamReader reader = new StreamReader("data//Bases.xml");
                XmlSerializer serializer = new XmlSerializer(typeof(List<Base>));
                Bases = (List<Base>)serializer.Deserialize(reader);
                reader.Close();
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
            LoadBases();
            LoadUsersInfo();
            LoadRecords();
        }

        public static bool ImportReportes(List<string> reportes)
        {
            try
            {
                foreach (string reporte in reportes)
                {
                    string[] data = reporte.Substring(0,reporte.Length-1).Split('#');
                    Guid UserId = Guid.Parse(data[0]);

                    string[] recordData = data[1].Split(';');
                    foreach (string s in recordData)
                    {
                        string[] inD = s.Split(':');
                        Record rec = new Record()
                        {
                            Id = Guid.Parse(inD[0]),
                            Date = inD[1],
                            AvtomatId = Guid.Parse(inD[2]),
                            IngredientId = Guid.Parse(inD[3]),
                            IngredientCount = int.Parse(inD[4]),
                            OperatorId = UserId

                        };
                        if(Records.Select(c=>c.Id).Contains(rec.Id)) continue;
                        Records.Add(rec);
                    }

                }
                SaveRecords();
                return true;
            }
            catch
            {
                return false;
            }
            
        }


    }


    public class Avtomat
    {

        public Guid Id { get; set; }

        public string Value { get; set; }
    }

    public class SendAvtomat
    {
        public string Id { get; set; }

        public string Value { get; set; }
    }

    public class SendIngredient
    {
        public string Id { get; set; }

        public string Value { get; set; }
    }

    public class SendUser
    {
        public string Id;

        public string Name;
    }

    public class Ingredient
    {
        public Guid Id { get; set; }

        public string Value { get; set; }

        public int Count { get; set; }
    }

    public class Record
    {

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

        public Guid OperatorId { get; set; }
    }

    /// <summary>
    /// Данные по складу
    /// </summary>
    public class Base
    {
        public Guid Id { get; set; }

        public Guid Ingredient { get; set; }

        public string Date { get; set; }

        public int Count { get; set; }

        /// <summary>
        /// Приход(true), Уход(false)
        /// </summary>
        public bool IsIn { get; set; }

        /// <summary>
        /// Кому выдано
        /// </summary>
        public Guid UserId { get; set; }
    }

    public class UserInfo
    {
        public UserInfo()
        {
            Id=Guid.NewGuid();
            Avtomats=new List<Guid>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int RoleName { get; set; }
        public string Password { get; set; }
        public List<Guid> Avtomats { get; set; }

    }
}
