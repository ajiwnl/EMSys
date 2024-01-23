using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class EnrollmentController : Controller
    {
        private List<string> edpCodesList = new List<string>();

        private readonly ApplicationDbContext _context;
        public EnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var enrollment = _context.Enrollment.ToList();
            return View(enrollment);
        }

        [HttpGet]
        public IActionResult Index(string searchStudentID)
        {
            var enrollq = from x in _context.Enrollment
                          select x;

            if (!string.IsNullOrEmpty(searchStudentID) && long.TryParse(searchStudentID, out long searchID))
            {
                enrollq = enrollq.Where(x => x.ENRHFSTUDID == searchID);
            }

            return View(enrollq.ToList());
        }
        [HttpGet]
        public IActionResult Edit(long? code)
        {
            var studid = _context.Enrollment.Where(s => s.ENRHFSTUDID == code).FirstOrDefault();
            return View(studid);
        }

        [HttpPost]
        public IActionResult Edit(Enrollment editid)
        {
            var studid = _context.Enrollment.FirstOrDefault(s => s.ENRHFSTUDID == editid.ENRHFSTUDID);

            if (studid != null)
            {
                _context.Enrollment.Remove(studid);
                _context.Add(editid);
                _context.SaveChanges();
                TempData["UpMessage"] = "Updated Successfully!";
                return RedirectToAction("Index");
            }
            return View(studid);
        }

        [HttpPost]
        public IActionResult Delete(long? code)
        {
            if (code == null)
            {
                return NotFound();
            }

            var studid = _context.Enrollment.FirstOrDefault(s => s.ENRHFSTUDID == code);

            if (studid == null)
            {
                return NotFound();
            }
            _context.Enrollment.Remove(studid);
            _context.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(Enrollment addRequestEnrollment)
        {
            string errorMsg = FieldValidation(addRequestEnrollment);
            if (!string.IsNullOrEmpty(errorMsg))
            {
                // Contains the error message to TempData from the FieldValidation function,
                //this will be use to display in JavaScript Pop-up box
                TempData["FieldError"] = errorMsg;
                return View("Add", addRequestEnrollment);
            }

            // Check if the primary key (ENRHFSTUDID) already exists
            if (_context.Enrollment.Any(s => s.ENRHFSTUDID == addRequestEnrollment.ENRHFSTUDID))
            {
                // The record already exists, this will be use to display in JavaScript Pop-up box
                TempData["DuplicateError"] = "Duplicate Error: The ID Number already exists. Cannot Proceed.";

                // Return the view with the model validation errors
                return View(addRequestEnrollment);
            }

            SaveData(addRequestEnrollment);
            TempData["SuccessMessage"] = "Student information saved successfully.";
            return View();
        }

        [HttpPost]
        [ActionName("CheckDuplicateId")]
        public IActionResult CheckDuplicateId(long studentId)
        {
            // Check if the student ID already exists
            bool isDuplicate = _context.Enrollment.Any(s => s.ENRHFSTUDID == studentId);

            return Json(new { success = !isDuplicate });
        }

        private void SaveData(Enrollment addRequestEnrollment)
        {
            // Check if the total units is less than or equal to 24
            if (addRequestEnrollment.ENRHFSTUDTOTALUNITS <= 24)
            {
                var enrollment = new Enrollment()
                {
                    ENRHFSTUDID = addRequestEnrollment.ENRHFSTUDID,
                    ENRHFSTUDENCODER = addRequestEnrollment.ENRHFSTUDENCODER,
                    ENRHFSTUDDATEENROLL = addRequestEnrollment.ENRHFSTUDDATEENROLL,
                    ENRHFSTUDTOTALUNITS = addRequestEnrollment.ENRHFSTUDTOTALUNITS,
                    ENRHFSTUDSCHLYR = "SY20xx-20xx",
                    ENRHFSTUDSTATUS = "EN"
                };
                _context.Enrollment.Add(enrollment);
                _context.SaveChanges();
            }
            else
            {
                // Handle the case where total units exceeds the limit of 24
                TempData["ErUnitsMessage"] = "Total units cannot exceed 24.";
            }
        }

        [HttpGet]
        [ActionName("CheckStudent")]
        public IActionResult CheckStudent(long studentId)
        {
            // First, check if the student with the given ID exists
            var student = _context.Student.FirstOrDefault(s => s.STFSTUDID == studentId);

            if (student != null)
            {
                // Concatenate the name from different columns
                string fullName = $"{student.STFSTUDLNAME}, {student.STFSTUDFNAME}, {student.STFSTUDMNAME}";
                // Return the student details
                return Json(new
                {
                    studentExists = true,
                    name = fullName,
                    year = student.STFSTUDYEAR,
                    course = student.STFSTUDCOURSE
                });
            }
            else
            {
                // If the student does not exist return a false JSON response
                return Json(new { studentExists = false });
            }
        }


        [HttpGet]
        [ActionName("CheckEDPCode")]
        public IActionResult CheckEDPCode(string edpCode)
        {
            // Check if the EDP code already exists in the list
            if (edpCodesList.Contains(edpCode))
            {
                return Json(new { edpCodeExists = true, alreadyAdded = true });
            }

            var subjectSched = _context.SSFSched.FirstOrDefault(s => s.SSFEDPCODE == edpCode);

            if (subjectSched != null)
            {
                var subjCode = subjectSched.SSFSUBJCODE;
                var subject = _context.Subject.FirstOrDefault(s => s.SUBJCODE == subjCode);

                if (subject != null)
                {

                    // Return the subject schedule details along with SUBJUNITS
                    return Json(new
                    {
                        edpCodeExists = true,
                        alreadyAdded = false,
                        edpCode = subjectSched.SSFEDPCODE,
                        subjCode = subjCode,
                        startTime = subjectSched.SSFSTARTTIME,
                        endTime = subjectSched.SSFENDTIME,
                        schedule = subjectSched.SSFDAYS,
                        room = subjectSched.SSFROOM,
                        units = subject.SUBJUNITS,
                    });

                }
            }

            return Json(new { edpCodeExists = false });
        }

        [HttpPost]
        [ActionName("UpClassSize")]
        public IActionResult UpClassSize(string edpCode)
        {
            var subjectSched = _context.SSFSched.FirstOrDefault(s => s.SSFEDPCODE == edpCode);


            //The first if-statment checks if the variable subjectSched 
            //is not null.Then, it retrieves the SSFSUBJCODE property from subjectSched 
            //and uses it to find a subject in the _context. 
            if (subjectSched != null)
            {
                var subjCode = subjectSched.SSFSUBJCODE;
                var subject = _context.Subject.FirstOrDefault(s => s.SUBJCODE == subjCode);

                //The second if-statement checks if  subject is found
                if (subject != null)
                {
                    // Increment the SSFCLASSSIZE by 1 for this EDP code
                    int? originalClassSize = subjectSched.SSFCLASSSIZE;

                    //it checks if the SSFCLASSSIZE property of subjectSched
                    //less than or equal to 40.If true, the class size will be incremented by 1, 
                    if (originalClassSize.HasValue && originalClassSize <= 40)
                    {
                        subjectSched.SSFCLASSSIZE = originalClassSize.Value + 1;

                        //save the changes to the _context, and returns a JSON object
                        //with success set to true and the updated class size.
                        _context.SaveChanges();
                        int newClassSize = subjectSched.SSFCLASSSIZE.Value;
                        return Json(new { success = true, classSize = newClassSize });
                    }
                    else
                    {
                        //Else the SSFCLASSSIZE limit is reached,
                        //it returns a JSON object with success set to false.
                        TempData["LimitMessage"] = "Class Size Limit reached. Can't Proceed";
                        return Json(new { success = false });
                    }
                }
            }

            // Return failure if the EDP code is not found
            return Json(new { success = false });
        }


        private string FieldValidation(Enrollment enrollment)
        {
            List<string> missingFields = new List<string>();
            string[] fieldNames = { "Encoder ID", "Enrolled Date", "Empty Enrollment Table" };

            if (string.IsNullOrEmpty(enrollment.ENRHFSTUDENCODER))
            {
                missingFields.Add(fieldNames[0]);
            }
            if (enrollment.ENRHFSTUDDATEENROLL == null || enrollment.ENRHFSTUDDATEENROLL == DateTime.MinValue)
            {
                missingFields.Add(fieldNames[1]);
            }
            if (enrollment.ENRHFSTUDTOTALUNITS == null || enrollment.ENRHFSTUDTOTALUNITS < 0.0)
            {
                missingFields.Add(fieldNames[2]);
            }

            if (missingFields.Count > 0)
            {
                string fields = string.Join(", ", missingFields);
                string message = $"The following fields are missing or have invalid values: {fields}";
                return message; // Return the error message with all missing fields
            }

            return null; // No errors, return null
        }
    }
}