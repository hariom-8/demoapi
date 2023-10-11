namespace CreateDemoApi.Models
{
    public class Author
    {
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }

        public List<Post> Posts { get; set; }
    }


}
