using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebDiary.Startup))]
namespace WebDiary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
