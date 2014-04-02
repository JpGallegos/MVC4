using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eManager.Web.Models
{
    public class EditEmployeeViewModel
    {
        [HiddenInput(DisplayValue=false)]
        public int Id { get; set; }

        [Required(ErrorMessage="Name is required.")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? HireDate { get; set; }
    }
}