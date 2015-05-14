using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Quiz.Web.Startup))]
namespace Quiz.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
