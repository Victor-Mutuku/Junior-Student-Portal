using Junior_Student_Portal.Models;
using Junior_Student_Portal.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection.Metadata.Ecma335;

namespace Junior_Student_Portal.Controllers
{   //For this controller is to handle HTTP requests,validate the model,call the repository method to save data and redirects or return a view
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;//stores the injected student repository.

        public StudentController(IStudentRepository studentRepository) //a constructor for the contoller(uses depedency injection).
        {
            _studentRepository = studentRepository;
        }

        public IActionResult Create() //handles the get requests.
        {
            return View();
        }

        [HttpPost]//handles post requests from the create form .
        public async Task<ActionResult> Create(Student student)
        {
            if (ModelState.IsValid)//checks if the iput data is valid based on validation rules in the student model.
            {
                await _studentRepository.AddAsync(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }
        [HttpGet]
        public async Task<IActionResult> GetStudents()//Either named "List"
        {
            var students = await _studentRepository.GetAllAsync();//have declared a variable that hold the object"Student".
            return View(students);
        }
        public async Task<IActionResult> Edit(int Id)//shows form first(gets specific student by Id).
        {
            var students = await _studentRepository.GetByIdAsync(Id);
            if (students == null) return NotFound();
            return View(students);
        }
        [HttpPost]//submits the form,typically handles the Post request when user submits the "Edit Form" and passes the data in as amodel.
        public async Task<IActionResult> Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                await _studentRepository.UpdateAsync(student);
                return RedirectToAction("Index");
            }
            return View("Edit","Students");//means if the data is invalid,Re-display the edit form with the user's data.
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int Id) //Deletes the student from the Database
        {
                 await _studentRepository.DeleteAsync(Id);

                return RedirectToAction("Index");   
        }
    }
}
