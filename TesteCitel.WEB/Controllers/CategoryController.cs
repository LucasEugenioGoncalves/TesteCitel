using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TesteCitel.Domain.Arguments.Category;
using TesteCitel.Domain.Interfaces.Services;
using TesteCitel.Infra.Transactions;
using TesteCitel.WEB.Controllers.Base;
using TesteCitel.WEB.Models;

namespace TesteCitel.WEB.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IServiceCategory _serviceCategory;

        public CategoryController(
             IUnitOfWork unitOfWork,
            IServiceCategory serviceCategory) : base(unitOfWork)
        {
            _serviceCategory = serviceCategory;
        }
        public async Task<IActionResult> Create()
        {
            var model = new CategoryViewModel()
            {
                Categories = await _serviceCategory.GetAllAsync(),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var request = new CreateCategoryRequest
                {
                    Name = model.Name
                };

                var response = await _serviceCategory.CreateAsync(request);

                if (_serviceCategory.Notifications.Any())
                    return BadRequest();

                await ResponseAsync(_serviceCategory);
                model.Categories = await _serviceCategory.GetAllAsync();
                return PartialView("_LoadTableCategory", model);
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        [HttpPut]
        public async Task<IActionResult> Alter([FromBody] CategoryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var request = new UpdateCategoryRequest
                {
                    Id = model.Id,
                    Name = model.Name,    
                };

                var response = await _serviceCategory.AlterAsync(request);

                if (_serviceCategory.Notifications.Any())
                    return BadRequest();

                await ResponseAsync(_serviceCategory);
                model.Categories = await _serviceCategory.GetAllAsync();

                return PartialView("_LoadTableCategory", model);
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

                var response = await _serviceCategory.RemoveAsync(id);

                if (_serviceCategory.Notifications.Any())
                    return BadRequest();

                await ResponseAsync(_serviceCategory);

                var model = new CategoryViewModel()
                {
                    Categories = await _serviceCategory.GetAllAsync(),
                };

                return PartialView("_LoadTableCategory", model);
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
