using System;
using System.Collections.Generic;

namespace RoundTheClock.Core.Model
{
    public class Customer
    {
        public string Name { get; set; }
        public List<Project> Projects { get; set; }

        public Customer() { }

        public Customer(string name)
        {
            Name = name;
        }
    }
}