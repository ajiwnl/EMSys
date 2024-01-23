using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EnrollmentDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var details = _context.EnrollmentDetails.ToList();
            return View(details);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Index(string searchStudentID)
        {
            var enrolldetailq = from x in _context.EnrollmentDetails
                              select x;

            if (!string.IsNullOrEmpty(searchStudentID) && long.TryParse(searchStudentID, out long searchID))
            {
                enrolldetailq = enrolldetailq.Where(x => x.ENRDFSTUDID == searchID);
            }

            return View(enrolldetailq.ToList());
        }
        [HttpGet]
        public IActionResult Edit(long? code)
        {
            var studid = _context.EnrollmentDetails.Where(s => s.ENRDFSTUDID == code).FirstOrDefault();
            return View(studid);
        }

        [HttpPost]
        public IActionResult Edit(EnrollmentDetails editid)
        {
            var studid = _context.EnrollmentDetails.FirstOrDefault(s => s.ENRDFSTUDID == editid.ENRDFSTUDID);

            if (studid != null)
            {
                _context.EnrollmentDetails.Remove(studid);
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

            var studid = _context.EnrollmentDetails.FirstOrDefault(s => s.ENRDFSTUDID == code);

            if (studid == null)
            {
                return NotFound();
            }
            _context.EnrollmentDetails.Remove(studid);
            _context.SaveChanges();
            TempData["DelMessage"] = "Deleted Successfully!";
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ActionName("AddDetail")]
        public IActionResult AddDetail([FromBody] EnrollmentDataModel model)
        {
            // Add a new record for each entry in the model
            foreach (var data in model.EnrollmentData)
            {
                // Check if both EDPCODE and SUBJECT CODE are not empty
                if (!string.IsNullOrEmpty(data.edpCode) && !string.IsNullOrEmpty(data.subjCode))
                {
                    // Check if a record with the same combination already exists
                    var existingRecord = _context.EnrollmentDetails
                        .FirstOrDefault(e => e.ENRDFSTUDID == model.StudentId && e.ENRDFSTUDEDPCODE == data.edpCode);

                    if (existingRecord == null)
                    {
                        // Record with the same combination does not exist, add a new record
                        var enrollmentDetail = new EnrollmentDetails
                        {
                            ENRDFSTUDID = model.StudentId,
                            ENRDFSTUDEDPCODE = data.edpCode,
                            ENRDFSTUDSUBJCODE = data.subjCode,
                            ENRDFSTUDSTATUS = "AC"
                        };

                        _context.EnrollmentDetails.Add(enrollmentDetail);
                    }
                    else { }
                }
            }

            _context.SaveChanges();

            return Ok();
        }
        public class EnrollmentDataModel
        {
            public long StudentId { get; set; }
            public List<EnrollmentData> EnrollmentData { get; set; }
        }

        public class EnrollmentData
        {
            public string edpCode { get; set; }
            public string subjCode { get; set; }
        }


    }
}
