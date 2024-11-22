using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Repositories.Interfaces
{
    public interface IPurchasedEbookRepository
    {
        
        Task AddPurchasedEbookAsync(PurchasedEbook purchasedEbook);
    }
}