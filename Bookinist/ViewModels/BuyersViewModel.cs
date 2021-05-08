using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using MathCore.WPF.ViewModels;

namespace Bookinist.ViewModels
{
    class BuyersViewModel : ViewModel
    {
        private readonly IRepository<Buyer> _buyers;

        public BuyersViewModel(IRepository<Buyer> Buyers)
        {
            _buyers = Buyers;
        }
    }
}
