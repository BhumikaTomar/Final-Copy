using BookStoreWebService.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookStoreWebService.Models.BookDB;
using BookStoreLibrary;
using System.Net.Http;

namespace BookStoreWebService.Models
{
    public class BookService
    {
        BookStoreDBContext context;
        public BookService()
        {
            context = new BookStoreDBContext();
        }
        public List<Book> SearchBooks(SubCategory s)
        {       
            
              var result = (from b in context.Book where b.CategoryId==s.CategoryId && b.SubCategoryId==s.SubCategoryId select b ).ToList();
            return result;
        }
        public List<Category> SelCategory()
        {

            var result = (from c in context.Category select c).ToList();
            return result;
        }

        public List<SubCategory> SelSubCategory(int cid)
        {
            var result = (from c in context.SubCategory where c.CategoryId == cid select c).ToList();
            return result;
        }
        public List<Book> AllBooks()
        {
            var result = (from b in context.Book select b).ToList();
            return result;
        }
    }
}
