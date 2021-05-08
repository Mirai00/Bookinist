using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Bookinist.Services.Interfaces;
using MathCore.WPF.Commands;

namespace Bookinist.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<Book> _booksRepository;
        private readonly IRepository<Seller> _sellersRepository;
        private readonly IRepository<Buyer> _buyersRepository;
        private readonly IRepository<Deal> _dealsRepository;
        private readonly ISalesService _salesService;

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

        #region CurrentViewModel : ViewModel - Текущая дочерняя модель-представления

        /// <summary>Текущая дочерняя модель-представления</summary>
        private ViewModel _CurrentViewModel;

        /// <summary>Текущая дочерняя модель-представления</summary>
        public ViewModel CurrentViewModel
        {
            get => _CurrentViewModel;
            set => Set(ref _CurrentViewModel, value);
        }

        #endregion

        #region Command ShowBooksViewCommand - Отобразить представление книг

        /// <summary>Отобразить представление книг</summary>
        private ICommand _ShowBooksViewCommand;

        /// <summary>Отобразить представление книг</summary>
        public ICommand ShowBooksViewCommand => _ShowBooksViewCommand
            ??= new LambdaCommand(OnShowBooksViewCommandExecuted, CanShowBooksViewCommandExecute);

        /// <summary>Проверка возможности выполнения - Отобразить представление книг</summary>
        private bool CanShowBooksViewCommandExecute() => true;

        /// <summary>Логика выполнения - Отобразить представление книг</summary>
        private void OnShowBooksViewCommandExecuted()
        {
            CurrentViewModel = new BooksViewModel(_booksRepository);
        }

        #endregion

        #region Command ShowBuyersViewCommand - Отобразить представление покупателей

        /// <summary>Отобразить представление покупателей</summary>
        private ICommand _ShowBuyersViewCommand;

        /// <summary>Отобразить представление покупателей</summary>
        public ICommand ShowBuyersViewCommand => _ShowBuyersViewCommand
            ??= new LambdaCommand(OnShowBuyersViewCommandExecuted, CanShowBuyersViewCommandExecute);

        /// <summary>Проверка возможности выполнения - Отобразить представление покупателей</summary>
        private bool CanShowBuyersViewCommandExecute() => true;

        /// <summary>Логика выполнения - Отобразить представление покупателей</summary>
        private void OnShowBuyersViewCommandExecuted()
        {
            CurrentViewModel = new BuyersViewModel(_buyersRepository);
        }

        #endregion

        #region Command ShowStatisticsViewCommand - Отобразить представление статистики

        /// <summary>Отобразить представление статистики</summary>
        private ICommand _ShowStatisticsViewCommand;

        /// <summary>Отобразить представление статистики</summary>
        public ICommand ShowStatisticsViewCommand => _ShowStatisticsViewCommand
            ??= new LambdaCommand(OnShowStatisticsViewCommandExecuted, CanShowStatisticsViewCommandExecute);

        /// <summary>Проверка возможности выполнения - Отобразить представление статистики</summary>
        private bool CanShowStatisticsViewCommandExecute() => true;

        /// <summary>Логика выполнения - Отобразить представление статистики</summary>
        private void OnShowStatisticsViewCommandExecuted()
        {
            CurrentViewModel = new StatisticsViewModel(_booksRepository, _sellersRepository, _buyersRepository,
                _dealsRepository);
        }

        #endregion

        public MainWindowViewModel(
            IRepository<Book> BooksRepository,
            IRepository<Seller> sellersRepository,
            IRepository<Buyer> buyersRepository,
            IRepository<Deal> DealsRepository,
            ISalesService SalesService)
        {
            _booksRepository = BooksRepository;
            _sellersRepository = sellersRepository;
            _buyersRepository = buyersRepository;
            _dealsRepository = DealsRepository;
            _salesService = SalesService;

            //Test();

        }

        private async void Test()
        {
            var dealsCount = _salesService.Deals.Count();

            var book = await _booksRepository.GetAsync(5);
            var buyer = await _buyersRepository.GetAsync(3);
            var seller = await _sellersRepository.GetAsync(7);

            var deal = _salesService.MakeDeal(book.Name, seller, buyer, 100m);
            var dealsCount2 = _salesService.Deals.Count();
        }
    }
}
