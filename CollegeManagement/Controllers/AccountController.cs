using Microsoft.AspNetCore.Mvc;
using CollegeManagement.Entities;
using CollegeManagement.Models;
using BCrypt.Net;
using System.Linq;

public class AccountController : Controller
{
    private readonly CollegeManagementDBContext _db;

    public AccountController(CollegeManagementDBContext db)
    {
        _db = db;
    }

    // Student Sign-up
    public IActionResult StudentSignUp() => View();

    [HttpPost]
    public IActionResult StudentSignUp(StudentSignUpModel model)
    {
        if (ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            Console.WriteLine("Validation Errors: " + string.Join(", ", errors));
            // Hash the password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // Map the model data to the StudentSignUp entity
            var students = new StudentSignUp
            {
                Name = model.Name,
                Email = model.Email,
                MobileNo = model.MobileNo,
                Gender = model.Gender,
                Address1 = model.Address1,
                Address2 = model.Address2,
                City = model.City,
                State = model.State,
                Password = hashedPassword
            };

            // Add the student to the database
            try
            {
                _db.StudentSignUps.Add(students); // Use StudentSignUps here
                _db.SaveChanges();
                return RedirectToAction("Login"); // Redirect to the login page after successful sign-up
            }
            catch (Exception ex)
            {
                // Log the full exception details, including the inner exception
                Console.WriteLine($"An error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                ModelState.AddModelError("", $"An error occurred while saving the student: {ex.Message}");
            }

        }

        // Return the view with validation errors
        return View(model);
    }



    // Teacher Sign-up
    public IActionResult TeacherSignUp() => View();

    [HttpPost]
    public IActionResult TeacherSignUp(TeacherSignUpModel model)
    {
        if (ModelState.IsValid)
        {
            var accessKey = _db.AccessKeys.FirstOrDefault(k => k.Key == model.AccessKey && !k.IsUsed);
            if (accessKey == null)
            {
                ModelState.AddModelError("", "Invalid or already used Access Key.");
                return View(model);
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var teacher = new TeacherSignUp
            {
                Name = model.Name,
                Email = model.Email,
                MobileNo = model.MobileNo,
                Gender = model.Gender,
                AccessKey = model.AccessKey,
                Password = hashedPassword
            };

            accessKey.IsUsed = true;
            _db.TeacherSignUps.Add(teacher);
            _db.SaveChanges();
            return RedirectToAction("Login");
        }
        return View(model);
    }

    // Login
    public IActionResult Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            // Find the login record by email
            var login = _db.Logins.FirstOrDefault(l => l.Email == model.Email);

            if (login != null && BCrypt.Net.BCrypt.Verify(model.Password, login.Password))
            {
                // Authentication succeeded, log the user in
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
            }
        }

        return View(model);
    }

}
