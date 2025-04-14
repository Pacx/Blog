using Blog.DAL.Infrastructure;
using Blog.DAL.Repository;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using Blog.DAL.Model;
using BlogCore.DAL.Tests;
using TDD.DbTestHelpers.Core;
using Microsoft.EntityFrameworkCore;

namespace Blog.DAL.Tests
{

    [TestClass]
    public class RepositoryTests: DbBaseTest<BlogFixtures>
    {

        public static string GetConnectionString(string name)
        {
            Configuration config =
            ConfigurationManager.OpenExeConfiguration(
            ConfigurationUserLevel.None);
            ConnectionStringsSection csSection =
            config.ConnectionStrings;
            for (int i = 0; i <
            ConfigurationManager.ConnectionStrings.Count; i++)
            {
                ConnectionStringSettings cs =
                csSection.ConnectionStrings[i];
                if (cs.Name == name)
                {
                    return cs.ConnectionString;
                }
            }
            return "";
        }

        [TestMethod]
        public void GetAllPost_TwoInDb_ReturnTwoPost()
        {
            // arrange
            String connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("BloggingDatabase");
            //String connectionString = GetConnectionString("BloggingDatabase");

            var context = new BlogContext(connectionString);
            context.Database.EnsureCreated();
            this.BaseFixtureSetUp();
            var repository = new BlogRepository(connectionString);
            
            // act
            var result = repository.GetAllPosts();
            // assert
            Assert.AreEqual(1, result.Count());
            this.BaseTearDown();
        }

        [TestMethod]
        [ExpectedException(typeof(Microsoft.EntityFrameworkCore.DbUpdateException))]
        public void ShouldNotAllowPostWithoutAuthor()
        {
            // arrange
            String connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("BloggingDatabase");
            //String connectionString = GetConnectionString("BloggingDatabase");

            var context = new BlogContext(connectionString);
            context.Database.EnsureCreated();

            var repository = new BlogRepository(connectionString);

            repository.AddPost(5,"Test content", null);

        }
        [TestMethod]
        
        public void ShouldBePlusOneRecord()
        {
            // arrange
            String connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("BloggingDatabase");
            //String connectionString = GetConnectionString("BloggingDatabase");

            var context = new BlogContext(connectionString);
            context.Database.EnsureCreated();

            var repository = new BlogRepository(connectionString);
            var resultBefoe = repository.GetAllPosts().ToList();
            repository.AddPost(5,"Test content", "123");
            var resultAfter = repository.GetAllPosts().ToList();
            Assert.AreEqual(resultBefoe.Count()+1, resultAfter.Count());

        }
        [TestMethod]
        public void GetPostWithComments_TwoPostInDb_ReturnOnePostWithComment()
        {
            // arrange
            String connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("BloggingDatabase");
            //String connectionString = GetConnectionString("BloggingDatabase");

            var context = new BlogContext(connectionString);
            context.Database.EnsureCreated();


            this.BaseFixtureSetUp();

            var repository = new BlogRepository(connectionString);
                
            // act
            var result = repository.GetAllPosts().ToList();
            
            // assert
            Assert.AreEqual(2, result.Count());
            repository.AddComment(2, "Second comment");

            foreach(var p in result)
            {
                var comment = repository.GetAllCommentsForPost(p.Id);

                Assert.AreEqual(1,comment.Count());

                

            }

            this.BaseTearDown();


        }
    }
}
