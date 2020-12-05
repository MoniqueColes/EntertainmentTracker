using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BooksAndMoviesMVC.Startup))]
namespace BooksAndMoviesMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
