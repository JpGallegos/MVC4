using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eManager.Domain;
using eManager.Web.Infrastructure;

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

        public PartialViewResult GridRender(int id)
        {
            var model = _db.Departments.Single(d => d.Id == id);
            return PartialView("_DepartmentGrid", model);
        }
    }
}
