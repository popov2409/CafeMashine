using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeMashine.Models
{
   public class Ingredient
   {
       public string Id { get; set; }
       public string Value { get; set; }

       public Ingredient()
       {
           Id = Guid.NewGuid().ToString();
       }

       public Ingredient(string id, string value)
       {
           Id = id;
           Value = value;
       }
   }
}
