using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eManager.Domain
{
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? HireDate { get; set; }
    }
}
