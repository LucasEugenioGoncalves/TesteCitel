using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TesteCitel.Domain.Arguments.Product;
using TesteCitel.Domain.Interfaces.Services;
using TesteCitel.Infra.Transactions;
using TesteCitel.WEB.Controllers.Base;
using TesteCitel.WEB.Models;

namespace TesteCitel.WEB.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IServiceProduct _serviceProduct;
        private readonly IServiceCategory _serviceCategory;

        public ProductController(
            IServiceProduct serviceProduct,
             IUnitOfWork unitOfWork,
            IServiceCategory serviceCategory) : base(unitOfWork)
        {
            _serviceProduct = serviceProduct;
            _serviceCategory = serviceCategory;
        }
        public async Task<IActionResult> Create()
        {
            var model = new ProductViewModel()
            {
                Products = await _serviceProduct.GetAllAsync(),
                Categories = await _serviceCategory.GetAllAsync(),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ProductViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var request = new CreateProductRequest
                {
                    Name = model.Name,
                    Price = Convert.ToDecimal(model.Price),
                    CategoryId = model.CategoryId
                };

                var response = await _serviceProduct.CreateAsync(request);
               
                if (_serviceProduct.Notifications.Any())
                    return BadRequest();

                await ResponseAsync(_serviceProduct);

                model.Products = await _serviceProduct.GetAllAsync();
                model.Categories = await _serviceCategory.GetAllAsync();

                return PartialView("_LoadTableProducts", model);
            }
            catch (System.Exception)
            {
                throw;
            }
         
        }

        [HttpPut]
        public async Task<IActionResult> Alter([FromBody] ProductViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var request = new UpdateProductRequest
                {
                    Id = model.Id,
                    Name = model.Name,
                    Price = Convert.ToDecimal(model.Price),
                    CategoryId = model.CategoryId
                };

                var response = await _serviceProduct.AlterAsync(request);

                if (_serviceProduct.Notifications.Any())
                    return BadRequest();

                await ResponseAsync(_serviceProduct);

                model.Products = await _serviceProduct.GetAllAsync();
                model.Categories = await _serviceCategory.GetAllAsync();

                return PartialView("_LoadTableProducts", model);
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        [HttpDelete]
        public async Task<IActionResult> Remove(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return BadRequest();
           
                var response = await _serviceProduct.RemoveAsync(id);

                if (_serviceProduct.Notifications.Any())
                    return BadRequest();

                await ResponseAsync(_serviceProduct);

                var model = new ProductViewModel()
                {
                    Products = await _serviceProduct.GetAllAsync(),
                    Categories = await _serviceCategory.GetAllAsync(),
                };

                return PartialView("_LoadTableProducts", model);
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
