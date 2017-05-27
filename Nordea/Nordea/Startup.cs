using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nordea.Startup))]
namespace Nordea
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
