using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookEventread.Startup))]
namespace BookEventread
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
