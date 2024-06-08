using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsPortal.web.Data;
using StudentsPortal.web.Models;
using StudentsPortal.web.Models.Entities;

namespace StudentsPortal.web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {

            var student = new Students

            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribe = viewModel.Subscribe
            };
            // inserting into student table in the database
            if (student is not null)
            {
             await dbContext.Student.AddAsync(student);
        
            await dbContext.SaveChangesAsync();

            }

            return RedirectToAction("List", "Students");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Student.ToListAsync();
            //var students = await dbContext.Student.ToArrayAsync();
            return View(students);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
           var student = await dbContext.Student.FindAsync(id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Students viewModel)
        {
           var student= await dbContext.Student.FindAsync(viewModel.Id);

            if (student is not null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribe = viewModel.Subscribe;
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Students viewModel)
        {
            var student = await dbContext.Student
                .AsNoTracking()
                .FirstOrDefaultAsync(x=> x.Id== viewModel.Id);

            if (student is not null)
            {
                dbContext.Student.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }

    }
}
