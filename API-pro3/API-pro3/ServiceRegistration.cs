using API_pro3.Data;
using API_pro3.Profiles;
using Microsoft.EntityFrameworkCore;

namespace API_pro3
{
    public static class ServiceRegistration
    {
        public static void AddService(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();
            //add this method after download automapper from nuget
            services.AddAutoMapper(opt =>
            {
               // opt.AddProfile(new MapperProfile(services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>()));
                    opt.AddProfile(new MapperProfile(new HttpContextAccessor()));
            });
        }
    }
}
