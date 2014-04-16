using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eManager.Domain;
using eManager.Web.Infrastructure;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace eManager.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentDataSource _db = new DepartmentDb();

        public ActionResult Detail(int id)
        {
            var model = _db.Departments.Single(d => d.Id == id);
            return View(model);
        }

        public ActionResult Department_Read (int id, [DataSourceRequest] DataSourceRequest request) {
            var  department = _db.Departments.Single(d => d.Id == id);
            IQueryable<Employee> employees = department.Employees.AsQueryable();
            // Deal with circular dependencies
            employees = employees.Select(x => new Employee() { 
                Id = x.Id,
                Name = x.Name,
                HireDate = x.HireDate,
                Department = null
            });
            DataSourceResult result = QueryableExtensions.ToDataSourceResult(employees, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
