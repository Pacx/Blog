using System.Collections.Generic;
using Blog.DAL.Infrastructure;
using Blog.DAL.Model;
using System;
using Microsoft.EntityFrameworkCore;
using BlogCore.DAL.Model;

namespace Blog.DAL.Repository
{
    public class BlogRepository
    {
        private readonly BlogContext _context;

        public BlogRepository(string connectionString)
        {
            _context = new BlogContext(connectionString);
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts;
        }
        public IEnumerable<Comment> GetAllCommentsForPost(long postId)
        {
            return _context.Comments.Where(x => x.PostId == postId);
        }

        public void AddPost(long Id, string content, string author)
        {
            Post post = new Post {Id=Id, Content = content, Author = author};
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
        public void AddComment(long postId,string content)
        {
            Comment comment = new Comment { Content = content, PostId = postId };
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}
