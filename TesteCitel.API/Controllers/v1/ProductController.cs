using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TesteCitel.API.Controllers.Base;
using TesteCitel.API.Models;
using TesteCitel.Domain.Arguments.Product;
using TesteCitel.Domain.Interfaces.Services;
using TesteCitel.Infra.Transactions;

namespace TesteCitel.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : BaseController
    {
        private readonly IServiceProduct _serviceProduct;
        private readonly IMapper _mapper;

        public ProductController(
            IServiceProduct serviceProduct,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _serviceProduct = serviceProduct;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Create([FromBody] CreateProductViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return ModelStateErrors();

                var createProductRequest = _mapper.Map<CreateProductRequest>(model);
                var response = await _serviceProduct.CreateAsync(createProductRequest);
                return ResponseAsync(response, _serviceProduct);
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Alter([FromBody] UpdateProductViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return ModelStateErrors();

                var updateProductRequest = _mapper.Map<UpdateProductRequest>(model);
                var response = await _serviceProduct.AlterAsync(updateProductRequest);
                return ResponseAsync(response, _serviceProduct);
            }
            catch (Exception ex)
            {
                return ResponseException(ex);
            }
        }

        [HttpDelete("Remove/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Remove(string id)
        {
            try
            {
                if (!ModelState.IsValid) return ModelStateErrors();

                var response = await _serviceProduct.RemoveAsync(id);
                return ResponseAsync(response, _serviceProduct);
            }
            catch (Exception ex)
            {
                return ResponseException(ex);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _serviceProduct.GetAllAsync();
                return ResponseAsync(response, _serviceProduct);
            }
            catch (Exception ex)
            {
                return ResponseException(ex);
            }
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var response = await _serviceProduct.GetByIdAsync(id);
                return ResponseAsync(response, _serviceProduct);
            }
            catch (Exception ex)
            {
                return ResponseException(ex);
            }
        }
    }
}
