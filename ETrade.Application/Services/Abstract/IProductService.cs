using ETrade.Application.DTOs.Product;

namespace ETrade.Application.Services.Abstract
{
    public interface IProductService
    {
        Task<List<ProductBaseDTO>> GetAll();

        Task<ProductDTO> GetByIdAsync(string id);

        Task AddAsync(CreateProductDTO productDTO);

        Task UpdateAsync(UpdateProductDTO productDTO);

        Task DeleteAsync(string id);
    }
}
