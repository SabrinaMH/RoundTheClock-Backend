using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundTheClock.Core.DAL
{
    public interface IRtcDbContext
    {
        DbSet<CustomerDAO> Customers { get; set; }
        DbSet<EntryDAO> Entries { get; set; }
        int SaveChanges();
    }
}
