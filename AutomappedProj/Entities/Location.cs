using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomappedProj.Entities
{
    public class Location
    {
        public virtual int Aisle { get; set; }
        public virtual int Shelf { get; set; }
    }
}
