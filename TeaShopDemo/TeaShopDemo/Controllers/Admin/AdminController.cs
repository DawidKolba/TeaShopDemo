﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TeaShopDemo.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {      
        public IActionResult Index()
        {
            return View();
        }
    }
}
