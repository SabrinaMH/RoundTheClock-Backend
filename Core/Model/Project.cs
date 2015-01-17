using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoundTheClock.Core.Model
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Customer Customer { get; set; }

        public Project() { }

        public Project(string name)
        {
            Name = name;
        }
    }
}