using Blog.DAL.Infrastructure;
using TDD.DbTestHelpers.Yaml;

namespace BlogCore.DAL.Tests
{
    public class BlogFixtures
     : YamlDbFixture<BlogContext, BlogFixturesModel>
    {
        public BlogFixtures()
        {
            SetYamlFolderName("C:\\Users\\Patryk\\Desktop\\Studia\\sab\\BlogCore\\BlogCore.DAL.Tests\\Fixtures\\");
            SetYamlFiles(["posts.yaml","comments.yaml"]);
        }
    }

}
