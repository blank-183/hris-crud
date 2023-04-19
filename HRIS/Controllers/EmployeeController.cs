using HRIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRIS.Data;

namespace SimpleHRIS.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HrissContext _context;

        public EmployeeController(HrissContext db)
        {
            this._context = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Employee> employees = this._context.Employees;
            return View(employees);
        }

        // Get
        public IActionResult Add()
        {
            return View();
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddEmployee employeePost)
        {
            if  (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    FirstName = employeePost.FirstName,
                    MiddleName = employeePost.MiddleName,
                    LastName = employeePost.LastName,
                    NameExtension = employeePost.NameExtension,
                    DateOfBirth = employeePost.DateOfBirth,
                    Sex = employeePost.Sex,
                    CivilStatus = employeePost.CivilStatus,
                    Height = employeePost.Height,
                    Weight = employeePost.Weight,
                    BloodType = employeePost.BloodType,
                };

                var govAccount = new GovernmentAccount()
                {
                    Employee = employee,
                    EmployeeId = employee.EmployeeId,
                    GsisNo = employeePost.GsisNo,
                    PagibigNo = employeePost.PagibigNo,
                    PhilhealthNo = employeePost.PhilhealthNo,
                    SssNo = employeePost.SssNo,
                    Tin = employeePost.Tin,
                };

                await this._context.AddAsync(govAccount);
                await this._context.SaveChangesAsync();

                TempData["success"] = "Employee added successfully!";

                return RedirectToAction("Index");
            }
            
            return View(employeePost);
        }

        [HttpGet]
        public IActionResult View(int? id)
        {
            
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var employee = this._context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            var govAccount = this._context.GovernmentAccounts.FirstOrDefault(g => g.EmployeeId == id);

            var backgroundList = this._context.EducationalBackgrounds.Where(x => x.EmployeeId == id).ToList();

            var employeeDetails = new EmployeeDetails()
            {
                Employee = employee,
                GovernmentAccount = govAccount,
                EducationalBackgrounds = backgroundList,
            };

            return View(employeeDetails);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            GovernmentAccount? govAccount = null;
            var employee = this._context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            govAccount = this._context.GovernmentAccounts.FirstOrDefault(g => g.EmployeeId == id);

            var model = new UpdateEmployee()
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                NameExtension = employee.NameExtension,
                DateOfBirth = employee.DateOfBirth,
                Sex = employee.Sex,
                CivilStatus = employee.CivilStatus,
                Height = employee.Height,
                Weight = employee.Weight,
                BloodType = employee.BloodType,
                Gsisno = govAccount.GsisNo,
                PagIbigNo = govAccount.PagibigNo,
                PhilHealthNo = govAccount.PhilhealthNo,
                Sssno = govAccount.SssNo,
                Tinno = govAccount.Tin,
            };
            
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UpdateEmployee model)
        {
            try
            {
                var employee = this._context.Employees.Find(model.EmployeeId);

                if (employee == null)
                {
                    return NotFound();
                }

                var govAccount = this._context.GovernmentAccounts.FirstOrDefault(g => g.EmployeeId == model.EmployeeId);

                employee.FirstName = model.FirstName;
                employee.MiddleName = model.MiddleName;
                employee.LastName = model.LastName;
                employee.NameExtension = model.NameExtension;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Sex = model.Sex;
                employee.CivilStatus = model.CivilStatus;
                employee.Height = model.Height;
                employee.Weight = model.Weight;
                employee.BloodType = model.BloodType;
                govAccount.GsisNo = model.Gsisno;
                govAccount.PagibigNo = model.PagIbigNo;
                govAccount.PhilhealthNo = model.PhilHealthNo;
                govAccount.SssNo = model.Sssno;
                govAccount.Tin = model.Tinno;

                this._context.SaveChanges();

                TempData["success"] = "Employee details updated successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.ToString();
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var employee = this._context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            var model = new UpdateEmployee()
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
            };

            return View(model);
        }

        // Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var employee = this._context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();

            }

            this._context.Employees.Remove(employee);
            this._context.SaveChanges();

            TempData["success"] = "Employee deleted successfully!";
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult AddEducation(int? id)
        {
            var employee = this._context.Employees.Find(id);
            
            if (employee == null)
            {
                return NotFound();
            }

            var model = new EducationalBackground()
            {
                EmployeeId = employee.EmployeeId,
            };

            return View(model);

        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEducation(EducationalBackground educBackground)
        {
            try
            {
                var employee = this._context.Employees.Find(educBackground.EmployeeId);

                if (employee == null)
                {
                    return NotFound();
                }

                var education = new EducationalBackground()
                {
                    EmployeeId = educBackground.EmployeeId,
                    Level = educBackground.Level,
                    NameOfSchool = educBackground.NameOfSchool,
                    Course = educBackground.Course,
                    FromDate = educBackground.FromDate,
                    ToDate = educBackground.ToDate,
                    UnitsEarned = educBackground.UnitsEarned,
                    YearGraduated = educBackground.YearGraduated,
                    HonorsReceived = educBackground.HonorsReceived,
                };

                this._context.Add(education);
                this._context.SaveChanges();

                TempData["success"] = "Educational background added successfully!";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message.ToString();
                return View(educBackground);
            }
        }

    }
}
