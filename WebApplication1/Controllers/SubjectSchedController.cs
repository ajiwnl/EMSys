using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebApplication1.Data;
using WebApplication1.Models;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
	public class SubjectSchedController : Controller
	{
		private readonly ApplicationDbContext _context;

		public SubjectSchedController(ApplicationDbContext context)
		{
			_context = context;
		}

        public IActionResult Index()
        {
            var subsched = _context.SSFSched.ToList();
            return View(subsched);
        }
        [HttpGet]
        public IActionResult Index(string searchEDPCode)
        {
            var ssffile = from x in _context.SSFSched
                          select x;

            if (!string.IsNullOrEmpty(searchEDPCode))
            {
                ssffile = ssffile.Where(x => x.SSFEDPCODE.Equals(searchEDPCode));
            }

            return View(ssffile.ToList());

        }

        [HttpGet]
        public IActionResult Edit(string? code)
        {
            var edpCode = _context.SSFSched.Where(s => s.SSFEDPCODE == code).FirstOrDefault();

            return View(edpCode);
        }

        [HttpPost]
        public IActionResult Edit(SubjectSched edpCodeEdit)
        {
            var edpcode = _context.SSFSched.FirstOrDefault(s => s.SSFEDPCODE == edpCodeEdit.SSFEDPCODE);

            if (edpcode != null)
            {
                _context.SSFSched.Remove(edpcode);
                _context.Add(edpCodeEdit);
                _context.SaveChanges();
                TempData["UpMessage"] = "Subject Schedule Info Updated Successfully!";
                return RedirectToAction("Index");
            }
            return View(edpcode);
        }
        [HttpPost]
        public IActionResult Delete(string? code)
        {
            if (code == null)
            {
                return NotFound();
            }

            var edpcode = _context.SSFSched.FirstOrDefault(s => s.SSFEDPCODE == code);

            if (edpcode == null)
            {
                return NotFound();
            }

            _context.SSFSched.Remove(edpcode);
            _context.SaveChanges();
            TempData["DelMessage"] = "Subject Schedule Deleted Successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            var courseToSubjectMap = GetCourseToSubjectMap();
            ViewBag.CourseToSubjectMap = JsonConvert.SerializeObject(courseToSubjectMap);
            ViewBag.CourseCodes = courseToSubjectMap.Keys; // Get the course codes for dropdown
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(SubjectSched addRequestSubSched)
        {
            string errorMsg = FieldValidation(addRequestSubSched);
            if (!string.IsNullOrEmpty(errorMsg))
            {
                TempData["FieldError"] = errorMsg;
                // Re-populate dropdown values before returning the view
                PopulateDropdownValues();
                return View("Add", addRequestSubSched);
            }

            if (_context.SSFSched.Any(s => s.SSFEDPCODE == addRequestSubSched.SSFEDPCODE))
            {
                TempData["DuplicateError"] = "Duplicate Error: The EDP Code already exists.";
                // Re-populate dropdown values before returning the view
                PopulateDropdownValues();
                return View(addRequestSubSched);
            }

            SaveData(addRequestSubSched);
            TempData["SuccessMessage"] = "Subject Schedule information saved successfully.";
            // Re-populate dropdown values before returning the view
            PopulateDropdownValues();
            return View();
        }

        private void PopulateDropdownValues()
        {
            var courseToSubjectMap = GetCourseToSubjectMap();
            ViewBag.CourseToSubjectMap = JsonConvert.SerializeObject(courseToSubjectMap);
            ViewBag.CourseCodes = courseToSubjectMap.Keys; // Get the course codes for dropdown
        }
        //This method The method uses the Any() method
        //to check if any subject in the collection meets the condition.
        //If a subject is found, the isValid variable is set to true, or return to false.
        [HttpPost]
        public IActionResult VerifySubject(string subjectCode, string courseCode)
        {
            var isValid = _context.Subject.Any(subject => subject.SUBJCOURSECODE == courseCode && subject.SUBJCODE == subjectCode);
            return Json(new { isValid });
        }

        [HttpGet]
        public IActionResult CheckSchedule(string startTime, string endTime, string days, string room)
        {
            if (!DateTime.TryParse(startTime, out DateTime parsedStartTime) ||
                !DateTime.TryParse(endTime, out DateTime parsedEndTime))
            {
                return Content("invaliddatetime");
            }

            string[] dayArray = days.Split(',');

            // Retrieve conflicting schedules from the database
            var potentialConflicts = _context.SSFSched
                .Where(s => s.SSFSTARTTIME < parsedEndTime && s.SSFENDTIME > parsedStartTime && s.SSFROOM == room)
                .ToList(); // Execute the query to retrieve the conflicting schedule

            // Check for conflict days conflict
            bool isConflict = potentialConflicts.Any(s => {
                string[] existingDayArray = s.SSFDAYS.Split(',');

                return DaysOverlap(dayArray, existingDayArray);
            });

            return isConflict ? Content("conflict") : Content("ok");
        }

        private bool DaysOverlap(string[] dayArray1, string[] dayArray2)
        {
            // Check each combination of days for overlaps
            foreach (var day1 in dayArray1)
            {
                foreach (var day2 in dayArray2)
                {
                    // Check if day1 is contained within day2 or vice versa
                    if (day1.Contains(day2) || day2.Contains(day1))
                    {
                        return true;
                    }
                }
            }
            return false;
        }



        //------------------------------------ Validation Functions------------------------------------------------------------
        private string FieldValidation(SubjectSched ssfSched)
        {
            List<string> missingFields = new List<string>();
            string[] fieldNames = { "EDP Code", "Course Code", "Subject Code", "Start Time", "End Time", "Schedule", "Days", "Room", "Section", "School Year" };

            if (string.IsNullOrEmpty(ssfSched.SSFEDPCODE))
            {
                missingFields.Add(fieldNames[0]);
            }
            if (string.IsNullOrEmpty(ssfSched.SSFCOURSECODE))
            {
                missingFields.Add(fieldNames[1]);
            }
            if (string.IsNullOrEmpty(ssfSched.SSFSUBJCODE))
            {
                missingFields.Add(fieldNames[2]);
            }
            if (ssfSched.SSFSTARTTIME == DateTime.MinValue)
            {
                missingFields.Add(fieldNames[3]);
            }
            if (ssfSched.SSFENDTIME == DateTime.MinValue)
            {
                missingFields.Add(fieldNames[4]);
            }
            if (string.IsNullOrEmpty(ssfSched.SSFXM))
            {
                missingFields.Add(fieldNames[5]);
            }
            if (string.IsNullOrEmpty(ssfSched.SSFDAYS))
            {
                missingFields.Add(fieldNames[6]);
            }
            if (string.IsNullOrEmpty(ssfSched.SSFROOM))
            {
                missingFields.Add(fieldNames[7]);
            }
            if (string.IsNullOrEmpty(ssfSched.SSFSECTION))
            {
                missingFields.Add(fieldNames[8]);
            }
            if (ssfSched.SSFSCHOOLYEAR == 0)
            {
                missingFields.Add(fieldNames[9]);
            }
            if (missingFields.Count > 0)
            {
                string fields = string.Join(", ", missingFields);
                string message = $"The following fields are missing or contain invalid values: {fields}.";
                return message;
            }
            return null;
        }

        private void SaveData(SubjectSched addRequestSubSched)
        {

            var subsched = new SubjectSched()
            {
                SSFEDPCODE = addRequestSubSched.SSFEDPCODE,
                SSFCOURSECODE = addRequestSubSched.SSFCOURSECODE,
                SSFSUBJCODE = addRequestSubSched.SSFSUBJCODE,
                SSFSTARTTIME = addRequestSubSched.SSFSTARTTIME,
                SSFENDTIME = addRequestSubSched.SSFENDTIME,
                SSFDAYS = addRequestSubSched.SSFDAYS,
                SSFROOM = addRequestSubSched.SSFROOM,
                SSFXM = addRequestSubSched.SSFXM,
                SSFMAXSIZE = 40,
                SSFCLASSSIZE = 0,
                SSFSTATUS = "AC",
                SSFSECTION = addRequestSubSched.SSFSECTION,
                SSFSCHOOLYEAR = addRequestSubSched.SSFSCHOOLYEAR
            };


            _context.SSFSched.Add(subsched);
            _context.SaveChanges();
        }

        private bool IsNumeric(string input)
        {
            return Regex.IsMatch(input, "^[0-9]+$");
        }

        //This method retrieve all the lists of subject codes from the database,
        //it initializes a dictionary to iterate all the subject code
        //and checks if the dictionary contains a specific SUBJCOURSECODE.
        private Dictionary<string, List<string>> GetCourseToSubjectMap()
        {
            var subjects = _context.Subject.ToList();

            var courseToSubjectMap = new Dictionary<string, List<string>>();

            foreach (var subject in subjects)
            {
                if (!courseToSubjectMap.ContainsKey(subject.SUBJCOURSECODE))
                {
                    courseToSubjectMap[subject.SUBJCOURSECODE] = new List<string>();
                }

                courseToSubjectMap[subject.SUBJCOURSECODE].Add(subject.SUBJCODE);
            }

            return courseToSubjectMap;
        }
    }
}
