using BookStoreLibrary;
using BookStoreWebService.Models.BookDB;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace BookStoreApplication.Models
{
    public class HomeService
    {
        HttpClient client;
        public HttpContext context;
        public HomeService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64966/");

        }
        public int AddRecord(Customer c)
        {
           
                string json = JsonConvert.SerializeObject(c);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage message = client.PostAsync("AdminService/AddNew", content).Result;
            if (message.IsSuccessStatusCode == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int ValidateUser(Credentials credentials)
        {
            byte[] ary;

         /*  bool isavailable = context.Session.TryGetValue("Customer", out ary);
            if (isavailable == false)
            {
                string Cust = JsonConvert.SerializeObject(credentials);
                context.Session.SetString("Customer", Cust);
            }*/
            string customerId;
            string json = JsonConvert.SerializeObject(credentials);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage message = client.PostAsync("AdminService/Authentication", content).Result;
            if (message.IsSuccessStatusCode == true)
            {
                customerId = message.Content.ReadAsStringAsync().Result;
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int ValidateAdmin(Credentials credentials)
        {
            
            string customerId;
            string json = JsonConvert.SerializeObject(credentials);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage message = client.PostAsync("AdminService/AuthenticationAdmin", content).Result;
            if (message.IsSuccessStatusCode == true)
            {
                customerId = message.Content.ReadAsStringAsync().Result;
                return 1;
            }
            else
            {
                return 0;
            }
        }



        public void AddBookRecord(Book b)
        {
            string json = JsonConvert.SerializeObject(b);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage message = client.PostAsync("AdminService/AddNewBook", content).Result;
            if (message.IsSuccessStatusCode == true)
            {

            }
        }
        public int RemoveBookRecord(int id)
        {
            string json = JsonConvert.SerializeObject(id);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage message = client.PostAsync("AdminService/RemoveBook", content).Result;
            if (message.IsSuccessStatusCode == true)
            {
                return 1;
            }
            else
                return 0;
        }
       
        public void EditBookRecord(Book b)
        {
            string json = JsonConvert.SerializeObject(b);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage message = client.PostAsync("AdminService/EditNewBook", content).Result;
            if (message.IsSuccessStatusCode == true)
            {

            }
        }
        public List<Book> GetAllBooks()
        {

            HttpResponseMessage message = client.PostAsync("AdminService/AllBooks", null).Result;

            if (message.IsSuccessStatusCode == true)
            {
                string json = message.Content.ReadAsStringAsync().Result.ToString();
                List<Book> allcategory = JsonConvert.DeserializeObject<List<Book>>(json);
                return allcategory;
            }
            else
                return null; ;
        }
    }
}
