using ETrade.Application.DTOs.Order;
using ETrade.Application.DTOs.Product;
using ETrade.Application.Services.Abstract;
using ETrade.Domain.Entities;
using ETrade.Domain.Repositories.Product;

namespace ETrade.Application.Services.Concrete
{
    public class ProductService : IProductService
    {
        protected readonly IProductReadRepository _productReadRepository;
        protected readonly IProductWriteRepository _productWriteRepository;

        public ProductService(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<List<ProductBaseDTO>> GetAll()
        {
            // IQueryable olduğu için ToList() ile liste haline getiriyoruz.
            var productEntities = _productReadRepository.GetAll().ToList();

            return productEntities.Select(product => new ProductBaseDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = (decimal)product.Price, // postgre sebebiyle hata fırlatıyor
                Stock = product.Stock,
                CreatedDate = product.CreatedDate,
                UpdatedDate = product.UpdatedDate
            }).ToList();
        }

        public async Task<ProductDTO> GetByIdAsync(string id)
        {
            var productEntity = await _productReadRepository.GetByIdAsync(id);

            if (productEntity == null)
            {
                throw new Exception("Product not found");
            }

            return new ProductDTO
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Price = (decimal)productEntity.Price,
                Stock = productEntity.Stock,
                CreatedDate = productEntity.CreatedDate,
                UpdatedDate = productEntity.UpdatedDate,
                Orders = productEntity.Orders?.Select(order => new OrderBaseDTO
                {
                    Id = order.Id,
                    Description = order.Description,
                    Address = order.Address,
                    CreatedDate = order.CreatedDate,
                    UpdatedDate = order.UpdatedDate
                }).ToList()
            };
        }

        public async Task AddAsync(CreateProductDTO productDTO)
        {
            var product = new Product
            {
                Name = productDTO.Name,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Orders = new List<Order>()
            };

            await _productWriteRepository.AddAsync(product);
            await _productWriteRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateProductDTO productDTO)
        {
            var product = await _productReadRepository.GetByIdAsync(productDTO.Id.ToString());

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            product.Name = productDTO.Name;
            product.Price = productDTO.Price;
            product.Stock = productDTO.Stock;
            product.UpdatedDate = DateTime.UtcNow;

            await _productWriteRepository.Update(product);
            await _productWriteRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            await _productWriteRepository.Delete(product);
            await _productWriteRepository.SaveChangesAsync();
        }
    }
}
