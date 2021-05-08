using System;
using System.Collections.Generic;
using System.Text;
using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Bookinist.DAL
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDb(this IServiceCollection services) => services
            .AddTransient<IRepository<Book>, BooksRepository>()
            .AddTransient<IRepository<Category>, DbRepository<Category>>()
            .AddTransient<IRepository<Seller>, DbRepository<Seller>>()
            .AddTransient<IRepository<Buyer>, DbRepository<Buyer>>()
            .AddTransient<IRepository<Deal>, DealsRepository>()
        ;
    }
}
