using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Bookinist.Services.Interfaces;

namespace Bookinist.Services
{
    class SalesService : ISalesService
    {
        private readonly IRepository<Book> _books;
        private readonly IRepository<Deal> _deals;

        public IEnumerable<Deal> Deals => _deals.Items;

        public SalesService(IRepository<Book> Books, IRepository<Deal> Deals)
        {
            _books = Books;
            _deals = Deals;
        }

        public async Task<Deal> MakeDeal(string BookName, Seller Seller, Buyer Buyer, decimal Price)
        {
            var book = _books.Items.FirstOrDefault(b => b.Name == BookName);
            if (book is null) return null;

            var deal = new Deal
            {
                Book = book,
                Buyer = Buyer,
                Seller = Seller,
                Price = Price
            };
            return await _deals.AddAsync(deal); 
        }
    }
}
