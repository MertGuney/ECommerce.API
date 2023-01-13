using ECommerce.Application.Abstractions;
using ECommerce.Persistance.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistance
{
    public static class PersistanceServiceRegistrations
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
