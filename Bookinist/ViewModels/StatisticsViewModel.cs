using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Bookinist.Services.Interfaces;
using MathCore.WPF.ViewModels;

namespace Bookinist.ViewModels
{
    class StatisticsViewModel : ViewModel
    {
        private readonly IRepository<Book> _books;
        private readonly IRepository<Seller> _sellers;
        private readonly IRepository<Buyer> _buyers;

        public StatisticsViewModel(
            IRepository<Book> Books,
            IRepository<Seller> Sellers,
            IRepository<Buyer> Buyers)
        {
            _books = Books;
            _sellers = Sellers;
            _buyers = Buyers;
        }
    }
}