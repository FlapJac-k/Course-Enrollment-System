using AutoMapper;
using CourseEnrollment.Business.Interfaces;
using CourseEnrollment.Data.Entities;
using CourseEnrollment.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseEnrollment.Controllers
{
    public class CoursesController(ICourseService courseService, IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var courses = await courseService.GetAllCoursesAsync();
            var viewModels = mapper.Map<IEnumerable<CourseViewModel>>(courses);
            return View(viewModels);
        }

        public async Task<IActionResult> Details(Guid id)
        {

            var course = await courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var viewModel = mapper.Map<CourseViewModel>(course);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var course = mapper.Map<Course>(viewModel);
                    await courseService.CreateCourseAsync(course);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var course = await courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var viewModel = mapper.Map<CourseViewModel>(course);
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(Guid id, CourseViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var course = mapper.Map<Course>(viewModel);
                    await courseService.UpdateCourseAsync(course);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
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
            var course = await courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var viewModel = mapper.Map<CourseViewModel>(course);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await courseService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableSlots(Guid courseId)
        {
            var slots = await courseService.GetAvailableSlotsAsync(courseId);
            return Json(new { availableSlots = slots });
        }
    }
}
