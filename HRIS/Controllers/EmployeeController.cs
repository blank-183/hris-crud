using HRIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRIS.Data;

namespace HRIS.Controllers
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
            try
            {
                IEnumerable<Employee> employees = this._context.Employees;
                return View(employees);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message.ToString();
                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpGet]
        public IActionResult Add()
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                TempData["error"] = ex.Message.ToString();
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddEmployee employeePost)
        {
            if (ModelState.IsValid)
            {
                try
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
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message.ToString();
                    return View(employeePost);
                }
            }
            TempData["error"] = "Error!";
            return View(employeePost);
        }

        [HttpGet]
        public IActionResult View(int? id)
        {
            try
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

                var backgroundList = this._context.EducationalBackgrounds.Where(x => x.EmployeeId == id).OrderByDescending(g => g.ToDate).ToList();

                var employeeDetails = new EmployeeDetails()
                {
                    Employee = employee,
                    GovernmentAccount = govAccount,
                    EducationalBackgrounds = backgroundList,
                };

                return View(employeeDetails);
            }
            catch(Exception ex)
            {
                TempData["error"] = ex.Message.ToString();
                return RedirectToAction("Index");
            }
            
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {
                GovernmentAccount? govAccount = null;
                var employee = this._context.Employees.Find(id);

                if (employee == null)
                {
                    return NotFound();
                }

                govAccount = this._context.GovernmentAccounts.FirstOrDefault(g => g.EmployeeId == id);

                var model = new ModifyEmployee()
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
            catch (Exception ex)
            {
                TempData["error"] = ex.Message.ToString();
                return RedirectToAction("Index");
            }
            

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ModifyEmployee model)
        {
            if(ModelState.IsValid)
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
                    return View(model);
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.ToString();
                    return View(model);
                }
            }
            TempData["error"] = "Error!";
            return View(model);

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            try
            {
                var employee = this._context.Employees.Find(id);

                if (employee == null)
                {
                    return NotFound();
                }

                var model = new ModifyEmployee()
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    MiddleName = employee.MiddleName,
                    LastName = employee.LastName,
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message.ToString();
                return RedirectToAction("Index");
            }
            
        }

        // Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            try
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
            catch (Exception ex)
            {
                TempData["error"] = ex.Message.ToString();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult AddEducation(int? id)
        {
            try
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
            catch (Exception ex)
            {
                TempData["error"] = ex.Message.ToString();
                return RedirectToAction("Index");
            }
            

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

                TempData["success"] = "Education added successfully!";
                return RedirectToAction("AddEducation");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message.ToString();
                return View(educBackground);
            }
        }

        [HttpGet]
        public IActionResult UpdateEducation(int? id)
        {
            try
            {
                var education = this._context.EducationalBackgrounds.Find(id);

                if (education == null)
                {
                    return NotFound();
                }

                var model = new EducationalBackground()
                {
                    EmployeeId = education.EmployeeId,
                    EducationalBackgroundId = education.EducationalBackgroundId,
                    Level = education.Level,
                    NameOfSchool = education.NameOfSchool,
                    Course = education.Course,
                    FromDate = education.FromDate,
                    ToDate = education.ToDate,
                    UnitsEarned = education.UnitsEarned,
                    YearGraduated = education.YearGraduated,
                    HonorsReceived = education.HonorsReceived,
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message.ToString();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateEducation(EducationalBackground educationPost)
        {
            try
            {
                var education = this._context.EducationalBackgrounds.Find(educationPost.EducationalBackgroundId);

                if (education == null)
                {
                    return NotFound();
                }
                education.Level = educationPost.Level;
                education.NameOfSchool = educationPost.NameOfSchool;
                education.Course = educationPost.Course;
                education.FromDate = educationPost.FromDate;
                education.ToDate = educationPost.ToDate;
                education.UnitsEarned = educationPost.UnitsEarned;
                education.YearGraduated = educationPost.YearGraduated;
                education.HonorsReceived = educationPost.HonorsReceived;

                this._context.SaveChanges();

                TempData["success"] = "Education updated successfully!";
                return View(education);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.ToString();
                return View(educationPost);
            }
        }

        [HttpGet]
        public IActionResult DeleteEducation(int? id)
        {
            try
            {
                var education = this._context.EducationalBackgrounds.Find(id);

                if (education == null)
                {
                    return NotFound();
                }

                var model = new EducationalBackground()
                {
                    EmployeeId = education.EmployeeId,
                    EducationalBackgroundId = education.EducationalBackgroundId,
                    Level = education.Level,
                    NameOfSchool = education.NameOfSchool,
                    Course = education.Course,
                    FromDate = education.FromDate,
                    ToDate = education.ToDate,
                    UnitsEarned = education.UnitsEarned,
                    YearGraduated = education.YearGraduated,
                    HonorsReceived = education.HonorsReceived,
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message.ToString();
                return RedirectToAction("Index");
            }

        }

        // Post
        [HttpPost, ActionName("DeleteEducation")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEducationPost(int? id)
        {
            try
            {
                var education = this._context.EducationalBackgrounds.Find(id);

                if (education == null)
                {
                    return NotFound();

                }

                this._context.EducationalBackgrounds.Remove(education);
                this._context.SaveChanges();

                TempData["success"] = "Education deleted successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message.ToString();
                return RedirectToAction("Index");
            }
        }

    }
}
