using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.DTOs
{
    public class ProductCount
    {
        public int Count { get ; set;}

        public long BookId{ get ; set ;}

        public ProductCount() { }
        public ProductCount(int count , long BookId){
            Count = count;
            BookId = BookId;
        }
        public Book Book {get; set ;}
    }
}