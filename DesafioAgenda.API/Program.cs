using DesafioAgenda.API.Configurations;
using DesafioAgenda.Infra.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerConfiguration();

// Configurando o CORS
builder.Services.ConfigureCors();

// Configurando o banco de dados
builder.Services.ConfigureDatabase(builder.Configuration);

// Registrando Handlers, Repositories e Services
builder.Services.AddHandlersAndRepositories();

// Configurando JWT
builder.Services.AddJwtConfiguration(builder.Configuration);

// Configurando FluentValidation
builder.Services.ConfigureFluentValidation();

// Acessor do HttpContext e outros serviços
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ValidationFilter>();

var app = builder.Build();

// Configurando CORS
app.UseCors("AllowLocalhost");

// Configurando Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUIConfiguration();
}

// Configurando the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();