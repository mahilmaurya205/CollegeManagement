using CollegeManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using CollegeManagement.Models;
using Microsoft.EntityFrameworkCore;

public class SubjectController : Controller
{
    private readonly CollegeManagementDBContext _db;

    public SubjectController(CollegeManagementDBContext db)
    {
        _db = db;
    }

    public ActionResult Index()
    {
        var subjectEntities = _db.Subjects.ToList();
        var subjectModels = subjectEntities.Select(s => new SubjectModel
        {
            SubjectID = s.SubjectID,
            SubjectName = s.SubjectName
            //RollNumber = s.RollNumber
            // Add other properties as needed
        }).ToList();

        return View(subjectModels);
    }

    public ActionResult Create() => View();

    [HttpPost]
    public ActionResult Create(Subject subject)
    {
        if (ModelState.IsValid)
        {
            _db.Subjects.Add(subject);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(subject);
    }

    // GET: Edit Subject
    public ActionResult Edit(int id)
    {
        var subject = _db.Subjects.FirstOrDefault(s => s.SubjectID == id);
        if (subject == null)
        {
            return NotFound();
        }

        var subjectModel = new SubjectModel
        {
            SubjectID = subject.SubjectID,
            SubjectName = subject.SubjectName
        };

        return View(subjectModel);
    }

    // POST: Save Edited Subject
    [HttpPost]
    public ActionResult Edit(SubjectModel subjectModel)
    {
        if (ModelState.IsValid)
        {
            var subject = _db.Subjects.FirstOrDefault(s => s.SubjectID == subjectModel.SubjectID);
            if (subject == null)
            {
                return NotFound();
            }

            subject.SubjectName = subjectModel.SubjectName;

            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(subjectModel);
    }

    // Delete Action to display delete confirmation page
    public ActionResult Delete(int id)
    {
        var subject = _db.Subjects.FirstOrDefault(s => s.SubjectID == id);
        if (subject == null)
        {
            return NotFound();
        }

        var subjectModel = new SubjectModel
        {
            SubjectID = subject.SubjectID,
            SubjectName = subject.SubjectName
        };

        return View(subjectModel);
    }

    // Post Action to handle deletion of a subject
    [HttpPost]
    [ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        var subject = _db.Subjects.FirstOrDefault(s => s.SubjectID == id);
        if (subject == null)
        {
            return NotFound();
        }

        _db.Subjects.Remove(subject);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
}
