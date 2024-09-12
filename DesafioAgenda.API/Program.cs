using DesafioAgenda.API.Configurations;
using DesafioAgenda.API.DataContext;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using DesafioAgenda.API.Validators;
using FluentValidation;
using DesafioAgenda.API.Filters;
using DesafioAgenda.API.Models;
using DesafioAgenda.API.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfiguration();
builder.Services.AddSwaggerGen();


// adcionando o CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("http://localhost:5173")  // Permitir o frontend
               .AllowAnyHeader()                     // Permitir qualquer cabeçalho
               .AllowAnyMethod();                    // Permitir qualquer método (GET, POST, etc.)
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddHandlersAndRepositories();

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddValidatorsFromAssemblyContaining<CreateContatoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateContatoValidator>();

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordHasher<UserModel>, PasswordHasher<UserModel>>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ValidationFilter>();


var app = builder.Build();

app.UseCors("AllowLocalhost");

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUIConfiguration();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
