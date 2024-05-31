using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management.Models;
namespace Student_Management.Controllers
{

    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["GpaSortOrder"] = string.IsNullOrEmpty(sortOrder) || sortOrder != "gpa_asc" ? "gpa_desc" : "gpa_asc";
            ViewData["NameSortOrder"] = sortOrder == "name_asc" ? "name_desc" : "name_asc";

            var students = from s in _context.Students
                           select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.FirstName.Contains(searchString) ||
                                               s.LastName.Contains(searchString) ||
                                               s.Address.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "gpa_asc":
                    students = students.OrderBy(s => s.GPA);
                    break;
                case "gpa_desc":
                    students = students.OrderByDescending(s => s.GPA);
                    break;
                case "name_desc":
                    students = students.OrderByDescending(s => s.FirstName);
                    break;
                default:
                    students = students.OrderBy(s => s.FirstName);
                    break;
            }

            return View(await students.ToListAsync());
        }




        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("StudentId,FirstName,LastName,Address,GPA")] Student student)
        {
            if (ModelState.IsValid)
            {
                if (_context.Students.Any(s => s.StudentId == student.StudentId))
                {
                    ModelState.AddModelError("StudentId", "A student with this ID already exists.");
                    return View(student);
                }

                _context.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,StudentId,FirstName,LastName,Address,GPA")] Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Students.Any(e => e.Id == student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
