using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoundTheClock.Core.Model
{
    public class Task
    {
        public string Name { get; set; }
        public int ProjectId { get; set; }

        public Task() { }

        public Task(string name)
        {
            Name = name;
        }
    }
}