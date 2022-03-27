using Microsoft.EntityFrameworkCore;
using prmToolkit.NotificationPattern;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteCitel.Domain.Arguments.Base;
using TesteCitel.Domain.Arguments.Category;
using TesteCitel.Domain.Entities;
using TesteCitel.Domain.Interfaces.Repositories;
using TesteCitel.Domain.Interfaces.Services;

namespace TesteCitel.Domain.Services
{
    public class ServiceCategory : Notifiable, IServiceCategory
    {
        private readonly IRepositoryCategory _repositoryCategory;
        public ServiceCategory(IRepositoryCategory repositoryCategory)
        {
            _repositoryCategory = repositoryCategory;
        }

        public async Task<ResponseBase> CreateAsync(CreateCategoryRequest request)
        {
            if (request is null)
            {
                AddNotification("Category", "Os dados do categoria é obrigatório.");
                return null;
            }

            var category = new Category(request.Name);
            
            AddNotifications(category);
            if (IsInvalid()) return null;

            await _repositoryCategory.InsertAsync(category);
            return new ResponseBase() { Id = category.Id.ToString() };
        }

        public async Task<ResponseBase> AlterAsync(UpdateCategoryRequest request)
        {
            if (request == null)
            {
                AddNotification("Category", "Os dados do categoria é obrigatório.");
                return null;
            }
            var category = await _repositoryCategory.GetByIdAsync(request.Id);
            if (category is null)
            {
                AddNotification("Category", "Os dados da categoria não foi encontrado.");
                return null;
            }
           
            category.Update(request.Name);
           
            AddNotifications(category);
            if (IsInvalid()) return null;

            _repositoryCategory.Update(category);
            return new ResponseBase() { Id = category.Id.ToString() };
        }

        public async Task<ResponseBase> RemoveAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                AddNotification("Category", "Os dados da categoria não foi encontrado.");
                return null;
            }

            var category = await _repositoryCategory.GetByIdAsync(id);
            if (category is null)
            {
                AddNotification("Category", "Os dados da categoria não foi encontrado.");
                return null;
            }

            _repositoryCategory.Remove(category);
            return new ResponseBase();
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
        {
            var products = await _repositoryCategory.GetAllBy().OrderBy(p => p.Name).ToListAsync();
            return products.Select(p => (CategoryResponse)p);
        }

        public async Task<CategoryResponse> GetByIdAsync(string id)
        {
            var product = await _repositoryCategory.GetByIdAsync(id);
            return (CategoryResponse)product;
        }
    }
}
