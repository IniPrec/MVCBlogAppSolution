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
        private readonly ICommentService _commentService;

        public BlogServiceTest()
        {
            _blogService = new BlogService();
            _commentService = new CommentService();
        }

        #region AddBlog

        // When AddBlogRequest is null, AddBlog should throw ArgumentNullException
        [Fact]
        public void AddBlog_NullBlog()
        {
            AddBlogRequest? blogAddRequest = null;

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

            BlogResponse response = _blogService.AddBlog(blogAddRequest);

            List<BlogResponse> blogs_from_GetAllBlogs = _blogService.GetAllBlogs();

            Assert.True(blogAddRequest.BlogTitle != Guid.Empty.ToString());
            Assert.Contains(response, blogs_from_GetAllBlogs);
        }

        #endregion

        #region GetAllBlogs

        [Fact]
        public void GetAllBlogs_EmptyList()
        {
            List<BlogResponse> blogs = _blogService.GetAllBlogs();

            Assert.Empty(blogs);
        }

        [Fact]
        public void GetAllBlogs_AddFewBlogs()
        {
            List<AddBlogRequest> blog_request_list = new List<AddBlogRequest>()
            {
                new AddBlogRequest()
                {
                    BlogTitle = "Sample Title 1",
                    BlogContent = "Sample content 1"
                },
                new AddBlogRequest()
                {
                    BlogTitle = "Sample Title 2",
                    BlogContent = "Sample content 2"
                }
            };

            List<BlogResponse> blogs = _blogService.GetAllBlogs();

            foreach (AddBlogRequest blogAddRequest in blog_request_list)
            {
                blogs.Add(_blogService.AddBlog(blogAddRequest));
            }

            List<BlogResponse> blogResponseList = _blogService.GetAllBlogs();

            foreach (BlogResponse blogResponse in blogResponseList)
            {
                Assert.Contains(blogResponse, blogResponseList);
            }
        }

        #endregion

        #region GetBlogByID

        [Fact]
        public void GetBlogByID_NullBlogID()
        {
            Guid? blogID = null;

            BlogResponse blogResponse_from_get_method = _blogService.GetBlogById(blogID);

            Assert.Null(blogResponse_from_get_method);
        }

        [Fact]
        public void GetBlogByID_ValidBlog()
        {
            AddBlogRequest? addBlogRequest = new AddBlogRequest() { BlogTitle = "How to create a blog", BlogContent = "This article shows you how to create a blog" };
            BlogResponse blogResponse_from_add_method = _blogService.AddBlog(addBlogRequest);


            BlogResponse? blogResponse_from_get_method = _blogService.GetBlogById(blogResponse_from_add_method.BlogId);

            Assert.Equal(blogResponse_from_add_method, blogResponse_from_get_method);
        }

        #endregion

        #region UpdateBlog

        [Fact]
        public void UpdateBlog_NullUpdate()
        {
            UpdateBlogRequest? updateBlogRequest = null;

            Assert.Throws<ArgumentNullException>(() => _blogService.UpdateBlog(updateBlogRequest));
        }

        [Fact]
        public void UpdateBlog_InvalidBlog()
        {
            UpdateBlogRequest updateBlogRequest = new UpdateBlogRequest()
            {
                BlogId = Guid.NewGuid(),
                BlogTitle = "New Title",
                BlogContent = "New Content"
            };
        }

        [Fact]
        public void UpdateBlog_ValidUpdate()
        {
            AddBlogRequest addBlogRequest = new AddBlogRequest()
            {
                BlogTitle = "Original Title",
                BlogContent = "Original Content"
            };

            BlogResponse addedBlog = _blogService.AddBlog(addBlogRequest);

            UpdateBlogRequest updatedBlogRequest = new UpdateBlogRequest()
            {
                BlogId = addedBlog.BlogId,
                BlogTitle = "Updated Title",
                BlogContent = "Updated Content"
            };

            BlogResponse updatedBlog = _blogService.UpdateBlog(updatedBlogRequest);

            Assert.Equal("Updated Title", updatedBlog.BlogTitle);
        }

        #endregion

        #region DeleteBlog

        [Fact]
        public void DeleteBlog_NullBlogId()
        {
            Guid? blogId = null;

            Assert.Throws<ArgumentNullException>(() => _blogService.DeleteBlog(blogId));
        }

        [Fact]
        public void DeleteBlog_InvalidBlog()
        {
            Guid? blogId = Guid.NewGuid();

            bool result = _blogService.DeleteBlog(blogId);

            Assert.False(result);
        }

        [Fact]
        public void DeleteBlog_ValidBlog()
        {
            AddBlogRequest addBlogRequest = new AddBlogRequest
            {
                BlogTitle = "To be Deleted Title",
                BlogContent = "To be Deleted Content"
            };

            BlogResponse addedBlog = _blogService.AddBlog(addBlogRequest);

            bool result = _blogService.DeleteBlog(addedBlog.BlogId);

            Assert.True(result);
        }

        #endregion

        #region AddComment

        [Fact]
        public void AddComment_NullComment()
        {
            AddCommentRequest? addCommentRequest = null;

            Assert.Throws<ArgumentNullException>(() => _commentService.AddComment(addCommentRequest));
        }

        #endregion
    }
}
