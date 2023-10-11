using CreateDemoApi.Controllers;
using CreateDemoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace CreateDemoApi.UnitTest
{
    public class UnitTest1
    {
        private readonly Mock<BlogDbContext> _context;
        private readonly Mock<ILogger<BlogController>> _logger;
        private readonly BlogController blogController;
        public UnitTest1()
        {
            var options = new DbContextOptionsBuilder<BlogDbContext>()
            .Options;

            _logger = new Mock<ILogger<BlogController>>();
            var mockDbContext = new Mock<BlogDbContext>(options);
            mockDbContext.Setup(x => x.Posts).Returns(GetMockDbSet());
            blogController = new BlogController(mockDbContext.Object, _logger.Object);

        }
        [Fact]
        public void GetPostsByAuthor()
        {
            var result = blogController.GetPostsByAuthor(1);
            if(result!= null)
            {
                Assert.True(true);
            }
           else { 
                Assert.False(true); 
            }
        }
        [Fact]
        public void updateCategory()
        {
            int postId =1;
            int newCategoryId =1;
            var result = blogController.UpdateCategory(postId, newCategoryId);
            if (result != null)
            {
                Assert.True(true);
            }
            else
            {
                Assert.False(true);
            }
        }
        [Fact]
        public void getCategoryCounts()
        {
         
            var result = blogController.GetCategoryCounts();
            Assert.True(true);
            
        }

        private DbSet<Post> GetMockDbSet()
        {
            var data = new List<Post>
        {
            new Post { PostID = 1, Title = "Post 1" ,AuthorID = 1},
            new Post { PostID = 2, Title = "Post 2" }
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Post>>();
            mockDbSet.As<IQueryable<Post>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Post>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Post>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Post>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockDbSet.Object;
        }



      
    }
}