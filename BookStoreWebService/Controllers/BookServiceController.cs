using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreLibrary;
using BookStoreWebService.Models;
using BookStoreWebService.Models.BookDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebService.Controllers
{
    [Route("BookService")]

    [ApiController]
    public class BookServiceController : ControllerBase
    {
        BookService service;
        public BookServiceController()
        {
            service = new BookService();
        }
        [Route("SearchBooks")]
        [HttpPost]
        public IActionResult SearchBooks(SubCategory s)
        {
            var result = service.SearchBooks(s);
            return Ok(result);
        }
        [HttpPost]
        [Route("SelCategory")]
        public List<Category> SelCategory()
        {
            // int id = 400;
            List<Category> allcat = service.SelCategory();
            return allcat;

        }
        [HttpGet]
        [Route("SelSubCategory")]
        public List<SubCategory> SelSubCategory(int cid)
        {
            List<SubCategory> allcat = service.SelSubCategory(cid);
            return allcat;
        }
        [HttpPost]
        [Route("AllBooks")]
        public List<Book> AllBooks()
        {

            List<Book> allbooks = service.AllBooks();
            return allbooks;

        }
      

    }
}