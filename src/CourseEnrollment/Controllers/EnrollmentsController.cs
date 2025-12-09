using AutoMapper;
using CourseEnrollment.Business.Interfaces;
using CourseEnrollment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseEnrollment.Controllers
{
    public class EnrollmentsController(
        IEnrollmentService enrollmentService,
        IStudentService studentService,
        ICourseService courseService,
        IMapper mapper) : Controller
    {

        public async Task<IActionResult> Index()
        {
            var enrollments = await enrollmentService.GetAllEnrollmentsAsync();
            var viewModels = mapper.Map<IEnumerable<EnrollmentViewModel>>(enrollments);
            return View(viewModels);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = await PrepareCreateEnrollmentViewModelAsync();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEnrollmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await enrollmentService.EnrollStudentAsync(viewModel.StudentId, viewModel.CourseId);

                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", result.ErrorMessage);
                }
            }

            await PopulateCreateEnrollmentViewModelAsync(viewModel);
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var enrollment = await enrollmentService.GetEnrollmentByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            var viewModel = mapper.Map<EnrollmentViewModel>(enrollment);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await enrollmentService.UnenrollStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableSlots(Guid courseId)
        {
            var slots = await courseService.GetAvailableSlotsAsync(courseId);
            return Json(new { availableSlots = slots });
        }

        private async Task<CreateEnrollmentViewModel> PrepareCreateEnrollmentViewModelAsync()
        {
            var viewModel = new CreateEnrollmentViewModel();
            await PopulateCreateEnrollmentViewModelAsync(viewModel);
            return viewModel;
        }

        private async Task PopulateCreateEnrollmentViewModelAsync(CreateEnrollmentViewModel viewModel)
        {
            var studentsTask = studentService.GetAllStudentsAsync();
            var coursesTask = courseService.GetAllCoursesAsync();

            await Task.WhenAll(studentsTask, coursesTask);

            viewModel.Students = mapper.Map<List<SelectListItem>>(studentsTask.Result);
            viewModel.Courses = mapper.Map<List<SelectListItem>>(coursesTask.Result);
        }
    }
}
