using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVCFluent.Models
{
    public class Context : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
    }
}