using DesafioAgenda.API.Handlers;
using DesafioAgenda.Domain.Validators;
using DesafioAgenda.Infra.DataContext;
using DesafioAgenda.Interface.IServices;
using DesafioAgenda.Service.Services;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using DesafioAgenda.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace DesafioAgenda.API.Configurations
{
    public static class DependencyInjection
    {
        public static void AddHandlersAndRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAgendaInterface, AgendaService>();
            services.AddScoped<CreateContatoHandler>();
            services.AddScoped<UpdateContatoHandler>();
            services.AddScoped<DeleteContatoHandler>();
            services.AddScoped<GetContatoByIdHandler>();
            services.AddScoped<GetAllContatosHandler>();
            services.AddScoped<CreateUserHandler>();
            services.AddScoped<LoginUserHandler>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IPasswordHasher<UserModel>, PasswordHasher<UserModel>>();
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost", builder =>
                {
                    builder.WithOrigins("http://localhost:5173")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
        }

        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());
            services.AddValidatorsFromAssemblyContaining<CreateContatoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateContatoValidator>();
        }
    }
}
