using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eManager.Domain;
using eManager.Web.Infrastructure;

namespace eManager.Web.Controllers
{
    public class DataController : Controller
    {
        private IDepartmentDataSource _db = new DepartmentDb();

        public ActionResult Departments()
        {
            var allDepartments = _db.Departments;
            return View(allDepartments);
        }

    }
}
