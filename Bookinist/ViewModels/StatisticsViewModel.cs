using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Bookinist.Services.Interfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bookinist.ViewModels
{
    class StatisticsViewModel : ViewModel
    {
        private readonly IRepository<Book> _books;
        private readonly IRepository<Seller> _sellers;
        private readonly IRepository<Buyer> _buyers;
        private readonly IRepository<Deal> _deals;
        private readonly ISalesService _salesService;

        #region BooksCount : int - Количество книг

        /// <summary>Количество книг</summary>
        private int _BooksCount;

        /// <summary>Количество книг</summary>
        public int BooksCount
        {
            get => _BooksCount;
            set => Set(ref _BooksCount, value);
        }

        #endregion

        #region Command ComputeStatisticCommand - Вычислить статистичекие данные

        /// <summary>Вычислить статистичекие данные</summary>
        private ICommand _ComputeStatisticCommand;

        /// <summary>Вычислить статистичекие данные</summary>
        public ICommand ComputeStatisticCommand => _ComputeStatisticCommand
            ??= new LambdaCommandAsync(OnComputeStatisticCommandExecuted, CanComputeStatisticCommandExecute);

        /// <summary>Проверка возможности выполнения - Вычислить статистичекие данные</summary>
        private bool CanComputeStatisticCommandExecute() => true;

        /// <summary>Логика выполнения - Вычислить статистичекие данные</summary>
        private async Task OnComputeStatisticCommandExecuted()
        {
            BooksCount = await _books.Items.CountAsync();

            var deals = _deals.Items;
            await deals.GroupBy(d => d.Book)
                .Select(bookDeals => new {Book = bookDeals.Key, Count = bookDeals.Count()})
                .OrderByDescending(book => book.Count)
                .Take(5)
                .ToArrayAsync();
                ;
        }

        #endregion

        public StatisticsViewModel(
            IRepository<Book> Books,
            IRepository<Seller> Sellers,
            IRepository<Buyer> Buyers,
            IRepository<Deal> Deals)
        {
            _books = Books;
            _sellers = Sellers;
            _buyers = Buyers;
            _deals = Deals;
        }
    }
}