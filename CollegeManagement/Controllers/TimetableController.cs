using CollegeManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using CollegeManagement.Models;
using System.Linq;

public class TimetableController : Controller
{
    private readonly CollegeManagementDBContext _db;

    // Constructor where CollegeManagementDBContext is injected via DI
    public TimetableController(CollegeManagementDBContext db)
    {
        _db = db;
    }

    // Index Action to display list of students
    public ActionResult Index()
    {
        var timetableEntities = _db.Timetables.ToList();
        var timetableModels = timetableEntities.Select(t => new TimetableModel
        {
            ClassID = t.ClassID,
            SubjectID = t.SubjectID,
            Day = t.Day,
            TimeSlot = t.TimeSlot,
            Room = t.Room
            // Add more properties if needed
        }).ToList();

        return View(timetableModels);
    }

    // Create Action to display create student form
    public ActionResult Create() => View();
    // Post Action to handle form submission
    [HttpPost]
    public ActionResult Create(TimetableModel timetableModel)
    {
        if (ModelState.IsValid)
        {
            var timetable = new Timetable
            {
                ClassID = timetableModel.ClassID,
                SubjectID = timetableModel.SubjectID,
                Day = timetableModel.Day ?? throw new ArgumentNullException("Day is required"),
                TimeSlot = timetableModel.TimeSlot ?? throw new ArgumentNullException("TimeSlot is required"),
                Room = timetableModel.Room ?? throw new ArgumentNullException("Room is required")
            };

            _db.Timetables.Add(timetable);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(timetableModel);
    }

    public ActionResult Edit(int id)
    {
        var timetable = _db.Timetables.FirstOrDefault(t => t.ClassID == id);
        if (timetable == null)
        {
            return NotFound();
        }

        var timetableModel = new TimetableModel
        {
            ClassID = timetable.ClassID,
            SubjectID = timetable.SubjectID,
            Day = timetable.Day,
            TimeSlot = timetable.TimeSlot,
            Room = timetable.Room
        };

        return View(timetableModel);
    }

    // Post Action to handle form submission and update timetable record
    [HttpPost]
    public ActionResult Edit(TimetableModel timetableModel)
    {
        if (ModelState.IsValid)
        {
            var timetable = _db.Timetables.FirstOrDefault(t => t.ClassID == timetableModel.ClassID);
            if (timetable != null)
            {
                timetable.SubjectID = timetableModel.SubjectID;
                timetable.Day = timetableModel.Day;
                timetable.TimeSlot = timetableModel.TimeSlot;
                timetable.Room = timetableModel.Room;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        return View(timetableModel);
    }
    // Delete Action to display delete confirmation page
    public ActionResult Delete(int id)
    {
        var timetable = _db.Timetables.FirstOrDefault(t => t.ClassID == id);
        if (timetable == null)
        {
            return NotFound();
        }

        var timetableModel = new TimetableModel
        {
            ClassID = timetable.ClassID,
            SubjectID = timetable.SubjectID,
            Day = timetable.Day,
            TimeSlot = timetable.TimeSlot,
            Room = timetable.Room
        };

        return View(timetableModel);
    }

    // Post Action to handle deletion of a timetable
    [HttpPost]
    [ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        var timetable = _db.Timetables.FirstOrDefault(t => t.ClassID == id);
        if (timetable == null)
        {
            return NotFound();
        }

        _db.Timetables.Remove(timetable);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

}
