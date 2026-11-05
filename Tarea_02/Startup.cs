using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tarea_02.Startup))]
namespace Tarea_02
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
