using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CafeMashine.Models
{
    public class Avtomat
    {
        public string Id { get; set; }
        public string Value { get; set; }

        public Avtomat()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Avtomat(string id,string value)
        {
            Id = id;
            Value = value;
        }
    }
}
