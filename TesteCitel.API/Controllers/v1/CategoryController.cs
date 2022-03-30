using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TesteCitel.API.Controllers.Base;
using TesteCitel.API.Models;
using TesteCitel.Domain.Arguments.Category;
using TesteCitel.Domain.Interfaces.Services;
using TesteCitel.Infra.Transactions;

namespace TesteCitel.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : BaseController
    {
        private readonly IServiceCategory _serviceCategory;
        private readonly IMapper _mapper;

        public CategoryController(
         IServiceCategory serviceCategory,
         IMapper mapper,
         IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _serviceCategory = serviceCategory;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Create([FromBody] CreateCategoryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return ModelStateErrors();

                var createProductRequest = _mapper.Map<CreateCategoryRequest>(model);
                var response = await _serviceCategory.CreateAsync(createProductRequest);
                return ResponseAsync(response, _serviceCategory);
            }
            catch (Exception ex)
            {
                return ResponseException(ex);
            }
        }

        [HttpPut]
        [Route("Alter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Alter([FromBody] UpdateCategoryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return ModelStateErrors();

                var updateCategoryRequest = _mapper.Map<UpdateCategoryRequest>(model);
                var response = await _serviceCategory.AlterAsync(updateCategoryRequest);
                return ResponseAsync(response, _serviceCategory);
            }
            catch (Exception ex)
            {
                return ResponseException(ex);
            }
        }

        [HttpDelete("Remove/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Remove(string id)
        {
            try
            {
                if (!ModelState.IsValid) return ModelStateErrors();

                var response = await _serviceCategory.RemoveAsync(id);
                return ResponseAsync(response, _serviceCategory);
            }
            catch (Exception ex)
            {
                return ResponseException(ex);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _serviceCategory.GetAllAsync();
                return ResponseAsync(response, _serviceCategory);
            }
            catch (Exception ex)
            {
                return ResponseException(ex);
            }
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var response = await _serviceCategory.GetByIdAsync(id);
                return ResponseAsync(response, _serviceCategory);
            }
            catch (Exception ex)
            {
                return ResponseException(ex);
            }
        }
    }
}
