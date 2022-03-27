using System.Collections.Generic;
using System.Threading.Tasks;
using TesteCitel.Domain.Arguments.Base;
using TesteCitel.Domain.Arguments.Product;
using TesteCitel.Domain.Interfaces.Services.Base;

namespace TesteCitel.Domain.Interfaces.Services
{
    public interface IServiceProduct : IServiceBase
    {
        Task<ResponseBase> CreateAsync(CreateProductRequest request);

        Task<ResponseBase> AlterAsync(UpdateProductRequest request);

        Task<ResponseBase> RemoveAsync(string id);

        Task<IEnumerable<ProductResponse>> GetAllAsync();

        Task<ProductResponse> GetByIdAsync(string id);
    }  
}
