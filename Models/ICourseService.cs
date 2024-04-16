namespace democode.Models
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAll();
        Course GetCourseById(string id);
        Course Add(Course course, string userName);
        Course Update(string id, Course course, string userName, string studentName);
        void Delete(string id);
    }
}
