using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using MathCore.WPF.ViewModels;

namespace Bookinist.ViewModels
{
    class BooksViewModel : ViewModel
    {
        private readonly IRepository<Book> _booksRepository;

        public BooksViewModel(IRepository<Book> BooksRepository)
        {
            _booksRepository = BooksRepository;
        }
    }
}