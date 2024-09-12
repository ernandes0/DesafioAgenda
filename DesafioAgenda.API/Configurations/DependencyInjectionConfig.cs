using DesafioAgenda.API.Handlers;
using DesafioAgenda.API.Services;

namespace DesafioAgenda.API.Configurations
{
    public static class DependencyInjectionConfig
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
        }
    }
}
