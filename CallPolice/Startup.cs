using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CallPolice.Startup))]
namespace CallPolice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}
