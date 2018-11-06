using System;
using System.Collections.Generic;
using BookStoreApplication.Models;
using BookStoreWebService.Models;
using BookStoreWebService.Models.BookDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApplication2.Models;

namespace BookStoreApplication.Controllers
{
    public class OrderController : Controller
    {
        BookManagerService BookAppservice;
        BookService service;
        ILogger<OrderController> log;
        public OrderController(ILogger<OrderController> log)
        {
            this.log = log;
            BookAppservice = new BookManagerService();
            service = new BookService();
        }
        [HttpPost][HttpGet]
        [ErrorFilter]
        public IActionResult AddToCart(ProductViewModel model)
        {
            try {
                log.LogInformation("Executing AddToCart Method..");
                log.LogInformation("This is a Test Message");
            }
            catch(Exception e) {
                log.LogCritical(e.Message);
                log.LogInformation("Executed AddToCart Method..");
            }
            
            BookAppservice.context = HttpContext;

            BookAppservice.AddToCart(model);

            string json = HttpContext.Session.GetString("CatSubCat");
            SubCategory subcategory = JsonConvert.DeserializeObject<SubCategory>(json);
            return RedirectToAction("SearchBooks", "Book", subcategory);


        }
        [HttpGet]
        [ErrorFilter]
        public IActionResult ViewCart()
        {
            try
            {
                log.LogInformation("Executing ViewCart Method..");
                log.LogInformation("This is a Test Message");
            }
            catch (Exception e)
            {
                log.LogCritical(e.Message);
                log.LogInformation("Executed ViewCart Method..");
            }
            BookAppservice.context = HttpContext;
            List<ProductViewModelCart> result = BookAppservice.ProductCart();
            ViewData["products"] = result;
            return View(result);
        }
        [HttpPost]
        [ErrorFilter]
        public IActionResult ProcessOrder(ProductViewModelCart[] p)
        {
            try
            {
                log.LogInformation("Executing ProcessOrder Method..");
                log.LogInformation("This is a Test Message");
            }
            catch (Exception e)
            {
                log.LogCritical(e.Message);
                log.LogInformation("Executed ProcessOrder Method..");
            }
            List<ProcessOrder> productList = new List<ProcessOrder>();
            foreach (var i in p)
            {
                ProcessOrder obj = new ProcessOrder();
                obj.BookId = i.BookId;
                obj.Price = i.Price;
                obj.sum = (int)(i.Price * i.Quantity);
                obj.Quantity = i.Quantity;
                obj.Title = i.Title;
                //if(productList.Find(obj))
                productList.Add(obj);


            }
            ViewData["products"] = productList;
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }

    }
}