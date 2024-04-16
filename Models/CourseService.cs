
using Newtonsoft.Json;
namespace democode.Models
{
    public class CourseService : ICourseService
    {
        public Course Add(Course course, string userName)
        {
            var courses = LoadCourseFromJson();
            
            Course newCourse = new Course
            {
                Id = course.Id,
                Name = course.Name,
                Teachers = new List<string> { userName }
            };
            courses.Add(newCourse);
            SaveCourseToJson(courses);
            return course;
        }

        public void Delete(string id)
        {
            var courses = LoadCourseFromJson();
            var course = courses.Where(c => c.Id == id).FirstOrDefault();
            courses.Remove(course);
            SaveCourseToJson(courses);
        }

        public IEnumerable<Course> GetAll()
        {
            var courses = LoadCourseFromJson();
            return courses;
        }

        public Course GetCourseById(string id)
        {
            var courses = LoadCourseFromJson();
            return courses.SingleOrDefault(c => c.Id == id);
        }

        public Course Update(string id, Course course,string teacherName, string studentName)
        {
            var courses = LoadCourseFromJson();
            var oldcourse = courses.FirstOrDefault(c => c.Id == id);
            var newCourse = courses.FirstOrDefault(u => u.Id == id);
            newCourse.Name = course.Name;
            newCourse.Teachers.Add(teacherName);
            newCourse.Students.Add(studentName);
            courses.Remove(oldcourse);
            courses.Add(newCourse);
            var json = System.IO.File.ReadAllText("json.json");
            var users = JsonConvert.DeserializeObject<List<User>>(json);
            var addCourse =  users.FirstOrDefault(u => u.Name == studentName);
            addCourse.Course = course.Name;
            var newjson = JsonConvert.SerializeObject(users);
            System.IO.File.WriteAllText("json.json", newjson);
            SaveCourseToJson(courses);
            return newCourse;
        }
        private List<Course> LoadCourseFromJson()
        {
            var json = System.IO.File.ReadAllText("courses.json");
            return JsonConvert.DeserializeObject<List<Course>>(json);
        }

        private void SaveCourseToJson(List<Course> courses)
        {
            var json = JsonConvert.SerializeObject(courses);
            System.IO.File.WriteAllText("courses.json", json);
        }
    }
}
