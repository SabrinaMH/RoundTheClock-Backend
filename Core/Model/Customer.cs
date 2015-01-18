using System;

namespace RoundTheClock.Core.Model
{
    public class Customer
    {
        public String Name { get; set; }

        public Customer() { }

        public Customer(string name)
        {
            Name = name;
        }
    }
}