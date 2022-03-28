﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TesteCitel.WEB.Models;

namespace TesteCitel.WEB.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {

            return Ok();
        }
    }
}
