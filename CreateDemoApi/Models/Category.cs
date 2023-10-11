namespace CreateDemoApi.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public List<Post> Posts { get; set; }
    }
}
