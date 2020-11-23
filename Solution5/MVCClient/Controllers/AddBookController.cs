﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MVCClient.Controllers
{
    public class AddBookController : Controller
    {   
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult GetDetails()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetDetails(Book b)
        {
           // _log4net.Info("Booking in progess");
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {
                using (var client = new HttpClient())
                {

                    StringContent content1 = new StringContent(JsonConvert.SerializeObject(b), Encoding.UTF8,"application/json");
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.PostAsync("https://localhost:44374/api/AddBook/", content1))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index","Book");

                        }
                        else
                        {
                            return RedirectToAction("GetDetails");
                        }
                        string apiResponse1 = await response.Content.ReadAsStringAsync();
                        
                    }
                    
                }
                return RedirectToAction("Index");
            }
        }
    }
}
