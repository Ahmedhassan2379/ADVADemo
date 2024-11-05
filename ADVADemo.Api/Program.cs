
using ADVADemo.Api.Extention;
using ADVADemo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ADVADemo.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddRepositoryConfiguration(builder.Configuration);

            //Passing ConnectionString Into ApplicationDbContext
            builder.Services.AddDbContext<EmployeeDbContext>(option => option
            .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(EmployeeDbContext).Assembly.FullName)));


            var app = builder.Build();

            #region UpdateDatabase Dynamic
            using var scope = app.Services.CreateScope();     //create scope that container all config services
            var services = scope.ServiceProvider;       //filter for services from this scope 
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = services.GetRequiredService<EmployeeDbContext>();    //Ask CLR to create object from DBContext
                await dbContext.Database.MigrateAsync();                      //update Database
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, ex.Message);
            }
            #endregion

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAllOrigins");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
