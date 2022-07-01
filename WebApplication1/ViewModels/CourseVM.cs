using System.Text.Json.Serialization;

namespace WebApplication1.ViewModels
{
    public class CourseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        //[JsonIgnore]
        public List<ProductVM> Products { get; set; }
    }
}
