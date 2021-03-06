﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;
using CafeMashine.Models;

namespace CafeMashine.Services
{
    public class DataProxy
    {
        private const string DATA_FOLDER_PATH = "data\\";
        private const string AVTOMAT_DATA_PATH = "avtomat.xml";
        private const string INGREDIENT_DATA_PATH = "ingredient.xml";
        private const string INGREDIENTCOUNT_DATA_PATH = "ingredientcount.xml";
        private const string USER_DATA_PATH = "user.xml";
        private const string RECORD_DATA_PATH = "record.xml";
        private const string USERAVTOMAT_DATA_PATH = "useravtomat.xml";

        private List<Avtomat> _avtomats;
        private List<Ingredient> _ingredients;
        private List<IngredientCount> _ingredientCounts;
        private List<User> _users;
        private List<Record> _records;
        private List<UserAvtomat> _userAvtomats;
        public DataProxy()
        {
            LoadAvtomats();
            LoadIngredients();
            LoadIngredientCounts();
            LoadUsers();
            LoadRecords();
            LoadUserAvtomats();
        }

        

        public List<Avtomat> Avtomats => _avtomats;
        public List<Ingredient> Ingredients => _ingredients;
        public List<IngredientCount> IngredientCounts => _ingredientCounts;
        public List<User> Users => _users;
        public List<Record> Records => _records;
        public List<UserAvtomat> UserAvtomats => _userAvtomats;

        private void LoadAvtomats()
        {
            _avtomats = new List<Avtomat>();
            try
            {
                StreamReader reader = new StreamReader(Path.Combine(DATA_FOLDER_PATH, AVTOMAT_DATA_PATH));
                XmlSerializer serializer = new XmlSerializer(typeof(List<Avtomat>));
                _avtomats = (List<Avtomat>) serializer.Deserialize(reader);
                reader.Close();
            }
            catch
            {
                // ignored
            }
        }

        private void SaveAvtomats()
        {
            StreamWriter writer = new StreamWriter(Path.Combine(DATA_FOLDER_PATH, AVTOMAT_DATA_PATH), false);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Avtomat>));
            serializer.Serialize(writer, _avtomats);
            writer.Close();
        }

        private void LoadIngredients()
        {
            _ingredients=new List<Ingredient>();
            try
            {
                StreamReader reader = new StreamReader(Path.Combine(DATA_FOLDER_PATH, INGREDIENT_DATA_PATH));
                XmlSerializer serializer = new XmlSerializer(typeof(List<Ingredient>));
                _ingredients = (List<Ingredient>)serializer.Deserialize(reader);
                reader.Close();
            }
            catch
            {
                // ignored
            }
        }

        private void SaveIngredients()
        {
            StreamWriter writer = new StreamWriter(Path.Combine(DATA_FOLDER_PATH, INGREDIENT_DATA_PATH), false);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Ingredient>));
            serializer.Serialize(writer, _ingredients);
            writer.Close();
        }

        private void LoadIngredientCounts()
        {
            _ingredientCounts = new List<IngredientCount>();
            try
            {
                StreamReader reader = new StreamReader(Path.Combine(DATA_FOLDER_PATH, INGREDIENTCOUNT_DATA_PATH));
                XmlSerializer serializer = new XmlSerializer(typeof(List<IngredientCount>));
                _ingredientCounts = (List<IngredientCount>)serializer.Deserialize(reader);
                reader.Close();
            }
            catch
            {
                // ignored
            }
        }

        private void SaveIngredientsCount()
        {
            StreamWriter writer = new StreamWriter(Path.Combine(DATA_FOLDER_PATH, INGREDIENTCOUNT_DATA_PATH), false);
            XmlSerializer serializer = new XmlSerializer(typeof(List<IngredientCount>));
            serializer.Serialize(writer, _ingredientCounts);
            writer.Close();
        }

        private void LoadUsers()
        {
            _users = new List<User>();
            try
            {
                StreamReader reader = new StreamReader(Path.Combine(DATA_FOLDER_PATH, USER_DATA_PATH));
                XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
                _users = (List<User>)serializer.Deserialize(reader);
                reader.Close();
            }
            catch
            {
                // ignored
            }
        }

        private void SaveUsers()
        {
            StreamWriter writer = new StreamWriter(Path.Combine(DATA_FOLDER_PATH, USER_DATA_PATH), false);
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            serializer.Serialize(writer, _users);
            writer.Close();
        }
        private void LoadRecords()
        {
            _records = new List<Record>();
            try
            {
                StreamReader reader = new StreamReader(Path.Combine(DATA_FOLDER_PATH, RECORD_DATA_PATH));
                XmlSerializer serializer = new XmlSerializer(typeof(List<Record>));
                _records = (List<Record>)serializer.Deserialize(reader);
            }
            catch
            {
                // ignored
            }
        }
        private void SaveRecords()
        {
            StreamWriter writer = new StreamWriter(Path.Combine(DATA_FOLDER_PATH, RECORD_DATA_PATH), false);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Record>));
            serializer.Serialize(writer, _records);
            writer.Close();
        }

        private void LoadUserAvtomats()
        {
            _userAvtomats = new List<UserAvtomat>();
            try
            {
                StreamReader reader = new StreamReader(Path.Combine(DATA_FOLDER_PATH, USERAVTOMAT_DATA_PATH));
                XmlSerializer serializer = new XmlSerializer(typeof(List<UserAvtomat>));
                _userAvtomats = (List<UserAvtomat>)serializer.Deserialize(reader);
                reader.Close();
            }
            catch
            {
                // ignored
            }
        }

        private void SaveUserAvtomats()
        {
            StreamWriter writer = new StreamWriter(Path.Combine(DATA_FOLDER_PATH, USERAVTOMAT_DATA_PATH), false);
            XmlSerializer serializer = new XmlSerializer(typeof(List<UserAvtomat>));
            serializer.Serialize(writer, _userAvtomats);
            writer.Close();
        }

        public void AddItem(object item)
        {
            switch (item)
            {
                case Avtomat avtomat:
                {
                    _avtomats.Add(avtomat);
                    SaveAvtomats();
                    break;
                }
                case Ingredient ingredient:
                {
                    _ingredients.Add(ingredient);
                    SaveIngredients();
                    break;
                }
                case IngredientCount ingredientCount:
                {
                    _ingredientCounts.Add(ingredientCount);
                    SaveIngredientsCount();
                    break;
                }
                case Record record:
                {
                    _records.Add(record);
                    SaveRecords();
                    break;
                }
                case User user:
                {
                    _users.Add(user);
                    SaveUsers();
                    break;
                }
                case UserAvtomat userAvtomat:
                {
                    _userAvtomats.Add(userAvtomat);
                    SaveUserAvtomats();
                    break;
                }
            }
        }

        public void UpdateItem(object item)
        {
            switch (item)
            {
                case Avtomat avtomat:
                {
                    var a= _avtomats.First(c => c.Id == avtomat.Id);
                    a.Value = avtomat.Value;
                    SaveAvtomats();
                    break;
                }
                case Ingredient ingredient:
                {
                    var i=_ingredients.First(c => c.Id == ingredient.Id);
                    i.Value = ingredient.Value;
                    i.Rank = ingredient.Rank;
                    SaveIngredients();
                    break;
                }
                case IngredientCount ingredientCount:
                {
                    _ingredientCounts.Remove(_ingredientCounts.First(c => c.Id == ingredientCount.Id));
                    _ingredientCounts.Add(ingredientCount);
                    SaveIngredientsCount();
                    break;
                }
                case Record record:
                {
                    _records.Remove(_records.First(c => c.Id == record.Id));
                    _records.Add(record);
                    SaveRecords();
                    break;
                }
                case User user:
                {
                    var u=_users.First(c => c.Id==user.Id);
                    u.Name = user.Name;
                    SaveUsers();
                    break;
                }
                case UserAvtomat userAvtomat:
                {
                    var ua = _userAvtomats.First(c => c.Id == userAvtomat.Id);
                    ua.User = userAvtomat.User;
                    ua.Avtomat = userAvtomat.Avtomat;
                    SaveUserAvtomats();
                    break;
                }
            }
        }

        public void RemoveItem(object item)
        {
            switch (item)
            {
                case Avtomat avtomat:
                {
                    _avtomats.Remove(_avtomats.First(c => c.Id == avtomat.Id));
                    SaveAvtomats();
                    break;
                }
                case Ingredient ingredient:
                {
                    _ingredients.Remove(_ingredients.First(c => c.Id == ingredient.Id));
                    SaveIngredients();
                    break;
                }
                case IngredientCount ingredientCount:
                {
                    _ingredientCounts.Remove(_ingredientCounts.First(c => c.Id == ingredientCount.Id));
                    SaveIngredientsCount();
                    break;
                }
                case Record record:
                {
                    _records.Remove(_records.First(c => c.Id == record.Id));
                    SaveRecords();
                    break;
                }
                case User user:
                {
                    _users.Remove(_users.First(c => c.Id == user.Id));
                    SaveUsers();
                    break;
                }
                case UserAvtomat userAvtomat:
                {
                    _userAvtomats.Remove(userAvtomat);
                    SaveUserAvtomats();
                    break;
                }
            }
        }
    }
}
