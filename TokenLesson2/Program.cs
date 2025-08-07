using Microsoft.EntityFrameworkCore;
using TokenLesson2.Common.Mappings;
using TokenLesson2.DataContext;
using TokenLesson2.Interface.Repository;
using TokenLesson2.Interface.Services;
using TokenLesson2.Repositories;
using TokenLesson2.Services;

namespace TokenLesson2;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddAuthorization();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IAuthRepository, AuthRepository>();
        builder.Services.AddTransient<IAuthService, AuthService>();
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddDbContext<AplicationDbContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseRouting();

        app.UseHttpsRedirection();

        app.UseAuthorization(); //

        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapControllers();

        app.Run();
    }
}


