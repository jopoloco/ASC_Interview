namespace WebApplication1.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<CourseVM> Courses { get; set; }
    }
}
