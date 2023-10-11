namespace CreateDemoApi.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorID { get; set; }
        public int CategoryID { get; set; }
        public DateTime PublicationDate { get; set; }

        public Author Author { get; set; }
        public Category Category { get; set; }
    }
}
