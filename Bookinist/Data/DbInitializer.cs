using Bookinist.DAL.Context;
using Bookinist.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bookinist.Data
{
    class DbInitializer
    {
        private BookinistDB _db;
        private ILogger<DbInitializer> _logger;

        public DbInitializer(BookinistDB db, ILogger<DbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация БД...");

            _logger.LogInformation("Уlадение существующей БД...");
            await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            _logger.LogInformation("Удадение существующей БД выполнено за {0} мс", timer.ElapsedMilliseconds);
            //_db.Database.EnsureCreated();
            _logger.LogInformation("Миграция БД...");
            await _db.Database.MigrateAsync();
            _logger.LogInformation("Миграция БД выполнено за {0} мс", timer.ElapsedMilliseconds);

            if (await  _db.Books.AnyAsync()) return;

            await InitializeCategories();
            await InitializeBooks();
            await InitializeSellers();
            await InitializeBuyers();
            await InitilizeDeals();
            _logger.LogInformation("Инициализация БД выполнено за {0} с", timer.Elapsed.TotalSeconds);
        }

        private const int _categoriesCount = 10;
        private Category[] _categories;

        private async Task InitializeCategories()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация Категорий...");
            _categories = new Category[_categoriesCount];
              for (var i = 0; i < _categoriesCount; i++)
                  _categories[i] = new Category {Name = $"Категория {i + 1}"};

              await _db.Categories.AddRangeAsync(_categories);
              await _db.SaveChangesAsync();
              _logger.LogInformation("Инициализация Категорий выполнено за {0} с", timer.Elapsed.TotalSeconds);
        }

        private const int _booksCount = 10;
        private Book[] _books;
        private async Task InitializeBooks()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация Книг...");
            var rnd = new Random();
            _books = Enumerable.Range(1, _booksCount)
                .Select(i => new Book
                {
                    Name = $"Книга{i}",
                    Category = rnd.NextItem(_categories)
                })
                .ToArray();
            await _db.Books.AddRangeAsync(_books);
            await _db.SaveChangesAsync();
            _logger.LogInformation("Инициализация Книг выполнено за {0} с", timer.Elapsed.TotalSeconds);
        }

        private const int _sellersCount = 10;
        private Seller[] _sellers;
        private async Task InitializeSellers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация Продавцов...");
            var rnd = new Random();
            _sellers = Enumerable.Range(1, _sellersCount)
                .Select(i => new Seller
                {
                    Name = $"Продавец-Имя {i}",
                    Surname = $"Продавец-Фамилия {i}",
                    Patronymic = $"Продавец-Отчество {i}",
                })
                .ToArray();
            await _db.Sellers.AddRangeAsync(_sellers);
            await _db.SaveChangesAsync();
            _logger.LogInformation("Инициализация Продавцов выполнено за {0} с", timer.Elapsed.TotalSeconds);
        }

        private const int _BuyersCount = 10;
        private Buyer[] _Buyers;
        private async Task InitializeBuyers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация Покупателей...");
            var rnd = new Random();
            _Buyers = Enumerable.Range(1, _BuyersCount)
                .Select(i => new Buyer
                {
                    Name = $"Покупатель-Имя {i}",
                    Surname = $"Покупатель-Фамилия {i}",
                    Patronymic = $"Покупатель-Отчество {i}",
                })
                .ToArray();
            await _db.Buyers.AddRangeAsync(_Buyers);
            await _db.SaveChangesAsync();
            _logger.LogInformation("Инициализация Покупателей выполнено за {0} с", timer.Elapsed.TotalSeconds);
        }

        private const int _dealsCount = 1000;
        private async Task InitilizeDeals()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация Сделок...");
            var rnd = new Random();
            var deals = Enumerable.Range(1, _dealsCount)
                .Select(i => new Deal
                {
                    Book = rnd.NextItem(_books),
                    Seller = rnd.NextItem(_sellers),
                    Buyer = rnd.NextItem(_Buyers),
                    Price = (decimal)(rnd.NextDouble() * 4000 + 700)
                });
            await _db.Deals.AddRangeAsync(deals);
            await _db.SaveChangesAsync();
            _logger.LogInformation("Инициализация Сделок выполнено за {0} с", timer.Elapsed.TotalSeconds);
        }
    }
}
