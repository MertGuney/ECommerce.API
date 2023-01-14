using ECommerce.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Persistance
{
    public static class PersistanceServiceRegistrations
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {

            services.AddDbContext<ECommerceDbContext>(opts =>
            {
                opts.UseNpgsql(Configuration.ConnectionString);
            });
        }
    }
}
