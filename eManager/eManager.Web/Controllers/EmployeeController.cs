using eManager.Domain;
using eManager.Web.Infrastructure;
using eManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eManager.Web.Controllers
{
    //[Authorize(Roles="Admin")]
    public class EmployeeController : Controller
    {
        private IDepartmentDataSource _db = new DepartmentDb();

        [HttpGet]
        [Authorize(Roles="Admin")]
        public ActionResult Create(int departmentId)
        {
            var model = new CreateEmployeeViewModel();
            model.DepartmentId = departmentId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateEmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var department = _db.Departments.Single(d => d.Id == viewModel.DepartmentId);
                var employee = new Employee();
                employee.Name = viewModel.Name;
                employee.HireDate = viewModel.HireDate;
                department.Employees.Add(employee);

                _db.Save();

                return RedirectToAction("detail", "department", new { id = viewModel.DepartmentId });
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var Employee = _db.Employees.Single(e => e.Id == id);
            return View(Employee);
        }

        [HttpGet]
        [Authorize(Roles="Admin")]
        public ActionResult Edit(int id)
        {
            var Employee = _db.Employees.Single(e => e.Id == id);

            if (Employee == null)
            {
                return HttpNotFound();
            }
            return View(Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _db.EntryChanged(employee);
                _db.Save();
                return RedirectToAction("Detail", new { id = employee.Id });
            }
            return View(employee);
        }

        [HttpGet]
        [Authorize(Roles="Admin")]
        public ActionResult Delete(int id)
        {
            Employee employee = _db.Employees.Single(e => e.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = _db.Employees.Single(e => e.Id == id);
            var deptId = employee.Department.Id;

            _db.RemoveEmployee(employee);
            _db.Save();
            return RedirectToAction("Detail", "Department", new { id = deptId});
        }
    }
}
