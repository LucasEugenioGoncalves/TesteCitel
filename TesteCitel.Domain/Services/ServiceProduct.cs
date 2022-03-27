using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteCitel.Domain.Arguments.Base;
using TesteCitel.Domain.Arguments.Product;
using TesteCitel.Domain.Entities;
using TesteCitel.Domain.Interfaces.Repositories;
using TesteCitel.Domain.Interfaces.Services;

namespace TesteCitel.Domain.Services
{
    public class ServiceProduct : Notifiable, IServiceProduct
    {
        private readonly IRepositoryProduct _repositoryProduct;
        public ServiceProduct(IRepositoryProduct repositoryProduct)
        {
            _repositoryProduct = repositoryProduct;
        }

        public async Task<ResponseBase> CreateAsync(CreateProductRequest request)
        {
            if (request == null)
            {
                AddNotification("Product", "Os dados do produto é obrigatório.");
                return null;
            }
            var produto = new Product(request.Name,request.Price,request.CategoryId);
           
            AddNotifications(produto);
            if (IsInvalid()) return null;

            await _repositoryProduct.InsertAsync(produto);
            return new ResponseBase() { Id = produto.Id.ToString() };
        }

        public async Task<ResponseBase> AlterAsync(UpdateProductRequest request)
        {
            if (request == null)
            {
                AddNotification("Product", "Os dados do produto é obrigatório.");
                return null;
            }
            var product = await _repositoryProduct.GetByAsync(where: p => p.Id == request.Id);
            if (product == null)
            {
                AddNotification("Product", "Os dados da produto não foi encontrado.");
                return null;
            }
           
            product.Update(request.Name,request.Price,request.CategoryId);
           
            AddNotifications(product);
            if (IsInvalid()) return null;

            _repositoryProduct.Update(product);
            return new ResponseBase() { Id = product.Id.ToString() };
        }

        public async Task<ResponseBase> RemoveAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                AddNotification("Product", "Os dados do produto não foi encontrado.");
                return null;
            }

            var product = await _repositoryProduct.GetByAsync(where: p => p.Id == id);
            if (product is null)
            {
                AddNotification("Product", "Os dados do produto não foi encontrado.");
                return null;
            }

            _repositoryProduct.Remove(product);
            return new ResponseBase();
        }

        public async Task<IEnumerable<ProductResponse>> GetAllAsync()
        {
            Func<IQueryable<Product>, IIncludableQueryable<Product, object>> include = s => s
                        .Include(x => x.Category);
            var products = await _repositoryProduct.GetAllBy(includeProperties: include).OrderBy(p => p.Name).ToListAsync();
            return products.Select(p => (ProductResponse)p);
        }

        public async Task<ProductResponse> GetByIdAsync(string id)
        {
            var product = await _repositoryProduct.GetByIdAsync(id,includeProperties: p => p.Include(x => x.Category));
            return (ProductResponse)product;
        }      
    }
}
