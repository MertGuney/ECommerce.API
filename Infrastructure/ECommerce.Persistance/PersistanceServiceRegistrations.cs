using ECommerce.Application.Repositories.Customers;
using ECommerce.Application.Repositories.File;
using ECommerce.Application.Repositories.InvoiceFile;
using ECommerce.Application.Repositories.Orders;
using ECommerce.Application.Repositories.ProductImageFile;
using ECommerce.Application.Repositories.Products;
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
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();

            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();


            services.AddDbContext<ECommerceDbContext>(opts => opts.UseNpgsql(Configuration.ConnectionString));
        }
    }
}
