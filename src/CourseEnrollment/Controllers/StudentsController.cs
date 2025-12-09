using AutoMapper;
using CourseEnrollment.Business.Interfaces;
using CourseEnrollment.Data.Entities;
using CourseEnrollment.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseEnrollment.Controllers
{
    public class StudentsController(IStudentService studentService, IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var students = await studentService.GetAllStudentsAsync();
            var viewModels = mapper.Map<IEnumerable<StudentViewModel>>(students);
            return View(viewModels);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var student = await studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var viewModel = mapper.Map<StudentViewModel>(student);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var student = mapper.Map<Student>(viewModel);
                    await studentService.CreateStudentAsync(student);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {

            var student = await studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var viewModel = mapper.Map<StudentViewModel>(student);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, StudentViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var student = mapper.Map<Student>(viewModel);
                    await studentService.UpdateStudentAsync(student);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
            return View(viewModel);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var student = await studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var viewModel = mapper.Map<StudentViewModel>(student);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await studentService.DeleteStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
