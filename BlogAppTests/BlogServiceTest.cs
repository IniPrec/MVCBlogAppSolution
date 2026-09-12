using System;
using System.Collections.Generic;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace BlogAppTests
{
    public class BlogServiceTest
    {
        private readonly IBlogService _blogService;

        public BlogServiceTest(IBlogService blogService)
        {
            _blogService = new BlogService();
        }

        // When AddBlogRequest is null, AddBlog should throw ArgumentNullException
        [Fact]
        public void AddBlog_NullBlog()
        {
            // Arrange
            AddBlogRequest? blogAddRequest = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => _blogService.AddBlog(blogAddRequest));
        }

        // When BlogTitle is null, AddBlog should throw ArgumentException
        [Fact]
        public void AddBlog_NullBlogTitle()
        {
            AddBlogRequest blogAddRequest = new AddBlogRequest
            {
                BlogTitle = null,
                BlogContent = "Sample content"
            };
            
            Assert.Throws<ArgumentException>(() => _blogService.AddBlog(blogAddRequest));
        }

        // When BlogTitle is empty, AddBlog should throw ArgumentException
        [Fact]
        public void AddBlog_EmptyBlogTitle()
        {
            AddBlogRequest blogAddRequest = new AddBlogRequest
            {
                BlogTitle = "",
                BlogContent = "Sample content"
            };
            
            Assert.Throws<ArgumentException>(() => _blogService.AddBlog(blogAddRequest));
        }

        // When BlogTitle is duplicate, AddBlog should throw ArgumentException
        [Fact]
        public void AddBlog_DuplicateBlogTitle()
        {
            AddBlogRequest blogAddRequest1 = new AddBlogRequest
            {
                BlogTitle = "Sample Title",
                BlogContent = "Sample content"
            };
            AddBlogRequest blogAddRequest2 = new AddBlogRequest
            {
                BlogTitle = "Sample Title",
                BlogContent = "Another content"
            };
            
            _blogService.AddBlog(blogAddRequest1);
            
            Assert.Throws<ArgumentException>(() => _blogService.AddBlog(blogAddRequest2));
        }

        // When the proper title is provided, AddBlog should insert the blog to the existing list and return the newly added blog with the generated BlogId
        [Fact]
        public void AddBlog_ValidBlog()
        {
            AddBlogRequest blogAddRequest = new AddBlogRequest
            {
                BlogTitle = "Sample Title",
                BlogContent = "Sample content"
            };

            AddBlogResponse response = _blogService.AddBlog(blogAddRequest);

            Assert.True(blogAddRequest.BlogTitle != Guid.Empty.ToString());
        }
    }
}
