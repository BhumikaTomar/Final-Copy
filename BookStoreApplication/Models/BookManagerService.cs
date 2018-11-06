using BookStoreWebService.Models.BookDB;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookStoreApplication.Models
{
    public class BookManagerService
    {
        HttpClient client;
        public HttpContext context;
        public BookManagerService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64966/");

        }
        public List<Category> SelectCategory()
        {
            HttpResponseMessage message = client.PostAsync("BookService/SelCategory", null).Result;

            if (message.IsSuccessStatusCode == true)
            {
                string json = message.Content.ReadAsStringAsync().Result.ToString();
                List<Category> allcategory = JsonConvert.DeserializeObject<List<Category>>(json);
                return allcategory;
            }
            else
                return null;
        }
        public List<Book> GetAllBooks()
        {

            HttpResponseMessage message = client.PostAsync("BookService/AllBooks", null).Result;

            if (message.IsSuccessStatusCode == true)
            {
                string json = message.Content.ReadAsStringAsync().Result.ToString();
                List<Book> allcategory = JsonConvert.DeserializeObject<List<Book>>(json);
                return allcategory;
            }
            else
                return null; ;
        }
        public void AddToCart(ProductViewModel model)
        {
            List<ProductViewModel> serializedObjects;
            string json = "";
            byte[] ary;
           
            // json = context.Session.GetString("CatSubCat");
            // SubCategory subcategory = JsonConvert.DeserializeObject<SubCategory>(json);
            bool isavailable = context.Session.TryGetValue("Cart", out ary);
            if (isavailable == false)
            {
                serializedObjects = new List<ProductViewModel>();
                serializedObjects.Add(model);
                serializedObjects=serializedObjects.Distinct().ToList();
                json = JsonConvert.SerializeObject(serializedObjects);
                context.Session.SetString("Cart", json);
            }
            else
            {
                json = context.Session.GetString("Cart");
                serializedObjects = JsonConvert.DeserializeObject<List<ProductViewModel>>(json);
                serializedObjects.Add(model);
                serializedObjects = serializedObjects.Distinct().ToList();
                json = JsonConvert.SerializeObject(serializedObjects);
                context.Session.SetString("Cart", json);
            }

        }
        public List<ProductViewModelCart> ProductCart()

        {

            // List<ProductViewModelCart> serializedObjects;
            string json = "";
            byte[] ary;
            bool isavailable = context.Session.TryGetValue("Cart", out ary);
            json = context.Session.GetString("Cart");
            //serializedObjects = JsonConvert.DeserializeObject<List<ProductViewModelCart>>(json);
            List<ProductViewModelCart> list = JsonConvert.DeserializeObject<List<ProductViewModelCart>>(json);

            List<ProductViewModelCart> result = (from c in list
                                                 select new ProductViewModelCart()
                                                 {
                                                     CategoryId = c.CategoryId,
                                                     Price = c.Price,
                                                    BookId = c.BookId,
                                                     Quantity = c.Quantity,
                                                     SubCategoryId = c.SubCategoryId,
                                                     Title = c.Title
                                                 }).ToList();

            return result.Distinct().ToList();
        }
    }


}
