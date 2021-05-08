using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookinist.DAL.Entities;

namespace Bookinist.Services.Interfaces
{
    interface ISalesService
    {
        Task<Deal> MakeDeal(string BookName, Seller Seller, Buyer Buyer, decimal Price);
        public IEnumerable<Deal> Deals { get; }
    }
}
