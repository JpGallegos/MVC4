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
        public ActionResult Create(int id)
        {
            var model = new CreateEmployeeViewModel();
            model.DepartmentId = id;
            model.HireDate = DateTime.Today;
            if (Request.IsAjaxRequest()) return PartialView("_CreatePartial", model);
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

                if (Request.IsAjaxRequest()) return Json(JsonResponseFactory.SuccessResponse(), JsonRequestBehavior.DenyGet);
                return RedirectToAction("detail", "department", new { id = viewModel.DepartmentId });
            }
            if (Request.IsAjaxRequest()) return Json(JsonResponseFactory.ErrorResponse("Please review your form."), JsonRequestBehavior.DenyGet);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var Employee = _db.Employees.Single(e => e.Id == id);
            if (Request.IsAjaxRequest()) return PartialView("_DetailPartial", Employee);
            return View(Employee);
        }

        [HttpGet]
        [Authorize(Roles="Admin")]
        public PartialViewResult Edit(int id)
        {
            var Employee = _db.Employees.Single(e => e.Id == id);
            
            var EmployeeEdit = new EditEmployeeViewModel();
            EmployeeEdit.Id = Employee.Id;
            EmployeeEdit.DepartmentId = Employee.Department.Id;
            EmployeeEdit.Name = Employee.Name;
            EmployeeEdit.HireDate = Employee.HireDate;
            return PartialView("_EditEmployeePartial", EmployeeEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(EditEmployeeViewModel employee)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                var EmployeeToUpdate = _db.Employees.Single(e => e.Id == employee.Id);
                EmployeeToUpdate.Name = employee.Name;
                _db.EntryChanged(EmployeeToUpdate);
                _db.Save();
                return Json(JsonResponseFactory.SuccessResponse(), JsonRequestBehavior.DenyGet);
            }
            return Json(JsonResponseFactory.ErrorResponse("Please review your form."), JsonRequestBehavior.DenyGet);
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
