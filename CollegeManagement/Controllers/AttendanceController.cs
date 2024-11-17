using CollegeManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using CollegeManagement.Models;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore.Metadata;

public class AttendanceController : Controller
{
    private readonly CollegeManagementDBContext _db;

    // Constructor where CollegeManagementDBContext is injected via DI
    public AttendanceController(CollegeManagementDBContext db)
    {
        _db = db;
    }

    // Index Action to display list of students
    public ActionResult Index()
    {
        var attendanceEntities = _db.Attendances.ToList();
        var subjectModels = attendanceEntities.Select(a => new AttendanceModel
        {
            AttendanceID = a.AttendanceID,
            SubjectID = a.SubjectID,
            Status = a.Status,
            Date = a.Date,
            StudentID = a.StudentID
            //SubjectName = s.SubjectName,
            //RollNumber = s.RollNumber
            // Add other properties as needed
        }).ToList();

        return View(subjectModels);
    }

    // Create Action to display create student form
    public ActionResult Create() => View();

    // Post Action to handle form submission
    [HttpPost]
    public ActionResult Create(AttendanceModel attendanceModel)
    {
        if (ModelState.IsValid)
        {
            var attendance = new Attendance
            {
                AttendanceID = attendanceModel.AttendanceID,
                StudentID = attendanceModel.StudentID,
                SubjectID = attendanceModel.SubjectID,
                Date = attendanceModel.Date,
                Status = attendanceModel.Status,
                // Add more properties if needed
            };

            _db.Attendances.Add(attendance);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(attendanceModel);
    }

    // GET: Edit Attendance
    public ActionResult Edit(int id)
    {
        var attendance = _db.Attendances.FirstOrDefault(a => a.AttendanceID == id);
        if (attendance == null)
        {
            return NotFound();
        }

        var attendanceModel = new AttendanceModel
        {
            AttendanceID = attendance.AttendanceID,
            StudentID = attendance.StudentID,
            SubjectID = attendance.SubjectID,
            Date = attendance.Date,
            Status = attendance.Status
        };

        return View(attendanceModel);
    }

    // POST: Save Edited Attendance
    [HttpPost]
    public ActionResult Edit(AttendanceModel attendanceModel)
    {
        if (ModelState.IsValid)
        {
            var attendance = _db.Attendances.FirstOrDefault(a => a.AttendanceID == attendanceModel.AttendanceID);
            if (attendance == null)
            {
                return NotFound();
            }

            attendance.StudentID = attendanceModel.StudentID;
            attendance.SubjectID = attendanceModel.SubjectID;
            attendance.Date = attendanceModel.Date;
            attendance.Status = attendanceModel.Status;

            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(attendanceModel);
    }

    // Delete Action to display delete confirmation page
    public ActionResult Delete(int id)
    {
        var attendance = _db.Attendances.FirstOrDefault(a => a.AttendanceID == id);
        if (attendance == null)
        {
            return NotFound();
        }

        var attendanceModel = new AttendanceModel
        {
            AttendanceID = attendance.AttendanceID,
            StudentID = attendance.StudentID,
            SubjectID = attendance.SubjectID,
            Date = attendance.Date,
            Status = attendance.Status
        };

        return View(attendanceModel);
    }

    // Post Action to handle deletion of an attendance record
    [HttpPost]
    [ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        var attendance = _db.Attendances.FirstOrDefault(a => a.AttendanceID == id);
        if (attendance == null)
        {
            return NotFound();
        }

        _db.Attendances.Remove(attendance);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
}
