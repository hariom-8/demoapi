using CreateDemoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace CreateDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly ILogger<BlogController> _logger;

        public BlogController(BlogDbContext context, ILogger<BlogController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Retrieve all posts written by a specific author
        [HttpGet("ByAuthor/{authorId}")]
        public IActionResult GetPostsByAuthor(int authorId)
        {
            try
            {
                _logger.LogInformation($"Request received to get posts by author ID {authorId}");

                var posts = _context.Posts.Where(p => p.AuthorID == authorId).ToList();

                if (posts == null || posts.Count == 0)
                {
                    _logger.LogWarning($"No posts found for the specified author ID {authorId}");
                    return NotFound("No posts found for the specified author.");
                }

                _logger.LogInformation($"Found {posts.Count} posts for author ID {authorId}");
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while processing request: {ex}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update the category of a specific post based on its ID
        [HttpPut("UpdateCategory/{postId}")]
        public IActionResult UpdateCategory(int postId, [FromBody] int newCategoryId)
        {
            try
            {
                _logger.LogInformation($"Request received to update category for post ID {postId}");

                var post = _context.Posts.FirstOrDefault(p => p.PostID == postId);

                if (post == null)
                {
                    _logger.LogWarning($"Post with ID {postId} not found");
                    return NotFound($"Post with ID {postId} not found.");
                }

                post.CategoryID = newCategoryId;
                _context.SaveChanges();

                _logger.LogInformation($"Category updated for post ID {postId}");
                return Ok(post);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while processing request: {ex}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Calculate the total number of posts in each category
        [HttpGet("CategoryCounts")]
        public IActionResult GetCategoryCounts()
        {
            try
            {
                _logger.LogInformation("Request received to get category counts");

                var categoryCounts = _context.Categories
                    .Include(c => c.Posts)
                    .Select(c => new
                    {
                        CategoryName = c.CategoryName,
                        TotalPosts = c.Posts.Count()
                    })
                    .ToList();

                _logger.LogInformation("Category counts retrieved successfully");
                return Ok(categoryCounts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while processing request: {ex}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
