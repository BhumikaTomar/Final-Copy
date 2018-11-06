using System;
using System.Collections.Generic;
using System.Linq;
using BookStoreApplication.Models;
using BookStoreWebService.Models;
using BookStoreWebService.Models.BookDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApplication2.Models;

namespace BookStoreApplication.Controllers
{

  

    public class BookController : Controller
    {
        ILogger<BookController> log;
        BookService service;
        BookManagerService serviceBook;   
        public BookController(ILogger<BookController> log)
        {
        
            this.log = log;
            service = new BookService();
            serviceBook = new BookManagerService();
        }
        [HttpPost][HttpGet]
        [ErrorFilter]
        public IActionResult SearchBooks(SubCategory s)
        {
            try
            {
                log.LogInformation("Executing SearchBooks method");
                log.LogInformation("This is a Test Message");
            }
            catch (Exception e)
            {
                log.LogCritical(e.Message);
                log.LogInformation("Executed SearchBooks Method..");
            }
            byte[] ary;
            serviceBook.context = HttpContext;
            string json = JsonConvert.SerializeObject(s);
            bool isavailable = serviceBook.context.Session.TryGetValue("CatSubCat", out ary);
            if (isavailable == false)
            {

               
                serviceBook.context.Session.SetString("CatSubCat", json);
            }
                var result = service.SearchBooks(s);


            List<ProductViewModel> model = (from p in result
                                            select new ProductViewModel()
                                            {
                                                BookId = p.BookId,
                                                CategoryId = p.CategoryId.Value,
                                                Price = p.Price.Value,
                                                Title = p.BookTitle,
                                                SubCategoryId = p.SubCategoryId.Value,
                                                Image = p.Image

                                            }).ToList();
            ViewData["products"] = model;
           
                   
            return View();
        }
        [ErrorFilter]
        public IActionResult Index()
        {
            try
            {
                log.LogInformation("Executing GetBooks method");
                log.LogInformation("This is a Test Message");
            }
            catch (Exception e)
            {
                log.LogCritical(e.Message);
                log.LogInformation("Executed GetBooks Method..");
            }
            List<Book> AllBookList = serviceBook.GetAllBooks();
            return View(AllBookList);
        }
        [ErrorFilter]
        public IActionResult GetBooks()
        {
            try
            {
                log.LogInformation("Executing GetBooks method");
                log.LogInformation("This is a Test Message");
            }
            catch (Exception e)
            {
                log.LogCritical(e.Message);
                log.LogInformation("Executed GetBooks Method..");
            }
            List<Book> AllBookList = serviceBook.GetAllBooks();       
            return View(AllBookList);
        }
        [ErrorFilter]
        public IActionResult GetCategory()
        {
            try {
                log.LogInformation("Executing GetCategory method");
                log.LogInformation("This is a Test Message");
            }
            catch (Exception e) {
                log.LogCritical(e.Message);
                log.LogInformation("Executed GetCategory Method..");
            }
            
            var resultlist = serviceBook.SelectCategory();
            SelectList list = new SelectList(resultlist, "CategoryId", "CategoryTitle");
            ViewBag.Categories = list;
            return View();
        }
    }
}