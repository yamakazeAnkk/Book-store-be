using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repositories.Interfaces;

namespace BookStore.Repositories
{
    public class PurchasedEbookRepository : IPurchasedEbookRepository
    {
        private readonly BookStoreContext _bookStoreContext;

        public PurchasedEbookRepository(BookStoreContext bookStoreContext){
            _bookStoreContext = bookStoreContext;
        }
        public async Task AddPurchasedEbookAsync(PurchasedEbook purchasedEbook)
        {
            _bookStoreContext.Add(purchasedEbook);
            await _bookStoreContext.SaveChangesAsync();
            
        }
    }
}