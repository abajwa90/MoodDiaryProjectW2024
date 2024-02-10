using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoodDiaryProjectW2024.Startup))]
namespace MoodDiaryProjectW2024
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
