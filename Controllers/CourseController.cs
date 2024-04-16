using democode.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web;
namespace democode.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _course;

        public CourseController(ICourseService course)
        {
            _course = course;
        }
        public IActionResult Index()
        {
            

            return View();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var courses = _course.GetAll();
            return View(courses);
        }
        [HttpGet]
        public IActionResult GetbyId(string id)
        {
            var course = _course.GetCourseById(id);
            return View(course);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Course course, string teacherName)
        {
            var newCourse = _course.Add(new Course
            {
                Id = course.Id,
                Name = course.Name,
            },teacherName);
            return RedirectToAction("Course","Dashboard");
        }
        [HttpPost]
        public IActionResult Update(string id,Course course,string teacherName, string studentName)
        {
            var newCourse = _course.Update(id, course, teacherName, studentName);
            return RedirectToAction("Course", "Dashboard");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            _course.Delete(id);
            return RedirectToAction("Course","Dashboard");
        }
    }
}
