using Microsoft.Extensions.DependencyInjection;
using Music.World.Service;

namespace Music.World.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("CorsPolicy", options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
            });
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IMusicService, MusicService>();
        }
    }
}
