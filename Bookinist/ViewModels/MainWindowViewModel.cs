using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookinist.DAL.Entities;
using Bookinist.Interfaces;

namespace Bookinist.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<Book> _booksRepository;
        #region Title : string - Заголовок

        /// <summary>Заголовок</summary>
        private string _Title = "Главное окно";

        /// <summary>Заголовок</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        public MainWindowViewModel(IRepository<Book> BooksRepository)
        {
            _booksRepository = BooksRepository;
            var books = BooksRepository.Items.Take(10).ToArray();
        }
    }
}
