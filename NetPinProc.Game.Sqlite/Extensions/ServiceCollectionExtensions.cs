using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NetPinProc.Game.Sqlite.Extensions
{
    /// <summary>
    /// Used for dependency injection services
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a NetProcDbContext and adds a INetProcDbContext service. <para/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddNetProcDataContext(this IServiceCollection services, string connectionString = null)
        {
            services.AddDbContext<NetProcDbContext>(opt =>
            {
                opt.UseSqlite(connectionString);
            });

            services.AddScoped<INetProcDbContext, NetProcDbContext>();

            return services;
        }
    }
}
