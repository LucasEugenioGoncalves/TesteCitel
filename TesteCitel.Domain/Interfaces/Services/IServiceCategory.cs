using System.Collections.Generic;
using System.Threading.Tasks;
using TesteCitel.Domain.Arguments.Base;
using TesteCitel.Domain.Arguments.Category;
using TesteCitel.Domain.Interfaces.Services.Base;

namespace TesteCitel.Domain.Interfaces.Services
{
    public interface IServiceCategory : IServiceBase
    {
        Task<ResponseBase> CreateAsync(CreateCategoryRequest request);

        Task<ResponseBase> AlterAsync(UpdateCategoryRequest request);

        Task<ResponseBase> RemoveAsync(string id);

        Task<IEnumerable<CategoryResponse>> GetAllAsync();

        Task<CategoryResponse> GetByIdAsync(string id);
    }
}
