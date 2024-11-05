using ADVADemo.Api.Helper;
using ADVADemo.Application.Interfaces;
using ADVADemo.Infrastructure.Repository;

namespace ADVADemo.Api.Extention
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection AddRepositoryConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod());
            });
            return services;

        }
    }
}
