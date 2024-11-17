using CollegeManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using CollegeManagement.Models;
using System.Linq;

public class StudentController : Controller
{
    private readonly CollegeManagementDBContext _db;

    // Constructor where CollegeManagementDBContext is injected via DI
    public StudentController(CollegeManagementDBContext db)
    {
        _db = db;
    }

    // Index Action to display list of students
    public ActionResult Index()
    {
        var studentEntities = _db.Students.ToList();
        var studentModels = studentEntities.Select(s => new StudentModel
        {
            ID = s.ID,
            Name = s.Name,
            RollNumber = s.RollNumber
            // Add other properties as needed
        }).ToList();

        return View(studentModels);
    }

    // Create Action to display create student form
    public ActionResult Create() => View();

    // Post Action to handle form submission
    [HttpPost]
    public ActionResult Create(StudentModel studentModel)
    {
        if (ModelState.IsValid)
        {
            var student = new Student
            {
                ID = studentModel.ID,
                Name = studentModel.Name,
                RollNumber = studentModel.RollNumber
                // Add more properties if needed
            };

            _db.Students.Add(student);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(studentModel);
    }

    // GET: Edit Student
    public ActionResult Edit(int id)
    {
        var student = _db.Students.FirstOrDefault(s => s.ID == id);
        if (student == null)
        {
            return NotFound();
        }

        var studentModel = new StudentModel
        {
            ID = student.ID,
            Name = student.Name,
            RollNumber = student.RollNumber
        };

        return View(studentModel);
    }

    // POST: Save Edited Student
    [HttpPost]
    public ActionResult Edit(StudentModel studentModel)
    {
        if (ModelState.IsValid)
        {
            var student = _db.Students.FirstOrDefault(s => s.ID == studentModel.ID);
            if (student == null)
            {
                return NotFound();
            }

            student.Name = studentModel.Name;
            student.RollNumber = studentModel.RollNumber;

            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(studentModel);
    }


    // Delete Action to display confirmation view for deletion
    public ActionResult Delete(int id)
    {
        var student = _db.Students.FirstOrDefault(s => s.ID == id);
        if (student == null)
        {
            return NotFound();
        }

        var studentModel = new StudentModel
        {
            ID = student.ID,
            Name = student.Name,
            RollNumber = student.RollNumber
            // Add more properties as needed
        };

        return View(studentModel);
    }

    // Post Action to confirm and handle deletion
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult ConfirmDelete(int id)
    {
        var student = _db.Students.FirstOrDefault(s => s.ID == id);
        if (student != null)
        {
            _db.Students.Remove(student);
            _db.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
