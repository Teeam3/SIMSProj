namespace democode.Models
{
    public class Course
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string>? Teachers { get; set; } = new List<string>();
        public List<string>? Students { get; set; } = new List<string>();
    }
}
