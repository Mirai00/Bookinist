using Bookinist.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Bookinist.DAL;

namespace Bookinist.Data
{
    static class DbRegistrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<BookinistDB>(option =>
            {
                var type = configuration["Type"];
                switch (type)
                {
                    case "MSSQL":
                        option.UseSqlServer(configuration.GetConnectionString(type));
                        break;
                    case "SQLite":
                        option.UseSqlite(configuration.GetConnectionString(type));
                        break;
                    case "InMemory":
                        option.UseInMemoryDatabase("Bookinist.db"); 
                        break;

                    case null: throw new InvalidOperationException($"Не определён тип БД");
                    default: throw new InvalidOperationException($"Тип подключения {type} не поддерживается");
                }
            })
            .AddTransient<DbInitializer>()
            .AddRepositoriesInDB()
        ;
    }
}
