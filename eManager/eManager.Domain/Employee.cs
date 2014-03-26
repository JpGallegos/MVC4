using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eManager.Domain
{
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        [Column(TypeName = "DateTime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public virtual DateTime? HireDate { get; set; }

        public virtual Department Department { get; set; }
    }
}
