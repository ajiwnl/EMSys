using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var students = _context.Student.ToList();
            return View(students);
        }
        [HttpGet]
        public IActionResult Index(string searchStudID)
        {
            var students = from x in _context.Student
                           select x;

            if (!string.IsNullOrEmpty(searchStudID) && long.TryParse(searchStudID, out long searchID))
            {
                students = students.Where(x => x.STFSTUDID == searchID);
            }

            return View(students.ToList());
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(Student addRequestStudent)
        {
            string errorMsg = FieldValidation(addRequestStudent);
            if (!string.IsNullOrEmpty(errorMsg))
            {
                // Contains the error message to TempData from the FieldValidation function,
                //this will be use to display in JavaScript Pop-up box
                TempData["FieldError"] = errorMsg;
                return View("Add", addRequestStudent);
            }

            // Check if the primary key (STFSTUDID) already exists
            if (_context.Student.Any(s => s.STFSTUDID == addRequestStudent.STFSTUDID))
            {
                // The record already exists, this will be use to display in JavaScript Pop-up box
                TempData["DuplicateError"] = "Duplicate Error: The ID Number already exists. Cannot Proceed.";

                // Return the view with the model validation errors
                return View(addRequestStudent);
            }
            SaveData(addRequestStudent);
            TempData["SuccessMessage"] = "Student information saved successfully.";
            return View();
        }

        [HttpGet]
        public IActionResult Edit(long? code)
        {
            var studid = _context.Student.Where(s => s.STFSTUDID == code).FirstOrDefault();

            return View(studid);
        }
        [HttpPost]
        public IActionResult Edit(Student studentEdit)
        {
            var student = _context.Student.FirstOrDefault(s => s.STFSTUDID == studentEdit.STFSTUDID);

            if (student != null)
            {
                _context.Student.Remove(student);
                _context.Add(studentEdit);
                _context.SaveChanges();
                TempData["UpMessage"] = "Student Info Updated Successfully!";
                return RedirectToAction("Index");
            }
            return View(studentEdit);
        }

        [HttpPost]
        public IActionResult Delete(long? code)
        {
            if (code == null)
            {
                return NotFound();
            }

            var student = _context.Student.FirstOrDefault(s => s.STFSTUDID == code);

            if (student == null)
            {
                return NotFound();
            }

            _context.Student.Remove(student);
            _context.SaveChanges();
            TempData["DelMessage"] = "Student Info Deleted Successfully!";
            return RedirectToAction("Index");
        }






















        //-------------------------------------------Functions-------------------------------------------//
        private string FieldValidation(Student student)
        {
            List<string> missingFields = new List<string>();
            string[] fieldNames = { "ID Number", "First Name", "Last Name", "Course", "Year", "Remarks" };
            if (!IsNumeric(student.STFSTUDID.ToString()))
            {
                missingFields.Add(fieldNames[0]);
            }
            if (student.STFSTUDID == 0L)
            {
                missingFields.Add(fieldNames[0]);
            }
            if (string.IsNullOrEmpty(student.STFSTUDFNAME) || !IsAlphabetOnly(student.STFSTUDFNAME))
            {
                missingFields.Add(fieldNames[1]);
            }
            if (string.IsNullOrEmpty(student.STFSTUDLNAME) || !IsAlphabetOnly(student.STFSTUDLNAME))
            {
                missingFields.Add(fieldNames[2]);
            }
            if (string.IsNullOrEmpty(student.STFSTUDCOURSE))
            {
                missingFields.Add(fieldNames[3]);
            }
            if (student.STFSTUDYEAR == 0)
            {
                missingFields.Add(fieldNames[4]);
            }
            if (string.IsNullOrEmpty(student.STFSTUDREMARKS))
            {
                missingFields.Add(fieldNames[5]);
            }

            if (missingFields.Count > 0)
            {
                string fields = string.Join(", ", missingFields);
                string message = $"The following fields are missing or invalid values: {fields}";
                return message; // Return the no input in fields error message
            }
            return null; // No errors, return null

        }

        private bool IsAlphabetOnly(string value)
        {
            // Checks if the input contains only alphabets (A-Z, a-z) or spaces
            return System.Text.RegularExpressions.Regex.IsMatch(value, "^[a-zA-Z ]+$");
        }

        private bool IsNumeric(string input)
        {
            return Regex.IsMatch(input, "^[0-9]+$");
        }

        private void SaveData(Student addRequestStudent)
        {
            // Check if STFSTUDMNAME is empty,IF YES, set the default value to n/a
            if (string.IsNullOrEmpty(addRequestStudent.STFSTUDMNAME))
            {
                addRequestStudent.STFSTUDMNAME = "n/a";
            }

            var student = new Student()
            {
                STFSTUDID = addRequestStudent.STFSTUDID,
                STFSTUDFNAME = addRequestStudent.STFSTUDFNAME,
                STFSTUDMNAME = addRequestStudent.STFSTUDMNAME,
                STFSTUDLNAME = addRequestStudent.STFSTUDLNAME,
                STFSTUDCOURSE = addRequestStudent.STFSTUDCOURSE,
                STFSTUDYEAR = addRequestStudent.STFSTUDYEAR,
                STFSTUDREMARKS = addRequestStudent.STFSTUDREMARKS,
                STFSTUDSTATUS = "AC"
            };

            _context.Student.Add(student);
            _context.SaveChanges();
        }

    }
}
