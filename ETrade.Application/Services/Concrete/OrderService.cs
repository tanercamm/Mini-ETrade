using ETrade.Application.DTOs.Customer;
using ETrade.Application.DTOs.Order;
using ETrade.Application.Services.Abstract;
using ETrade.Domain.Entities;
using ETrade.Domain.Repositories.Order;

namespace ETrade.Application.Services.Concrete
{
    public class OrderService : IOrderService
    {
        protected readonly IOrderReadRepository _orderReadRepository;
        protected readonly IOrderWriteRepository _orderWriteRepository;

        public OrderService(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task<List<OrderBaseDTO>> GetAll()
        {
            // IQueryable olduğu için ToList() ile liste haline getiriyoruz.
            var orderEntities = _orderReadRepository.GetAll().ToList();

            return orderEntities.Select(order => new OrderBaseDTO
            {
                Id = order.Id,
                Description = order.Description,
                Address = order.Address,
                CreatedDate = order.CreatedDate,
                UpdatedDate = order.UpdatedDate
            }).ToList();
        }

        public async Task<OrderDTO> GetByIdAsync(string id)
        {
            var orderEntity = await _orderReadRepository.GetByIdAsync(id);

            if (orderEntity == null)
            {
                throw new Exception("Order not found");
            }

            return new OrderDTO
            {
                Id = orderEntity.Id,
                Description = orderEntity.Description,
                Address = orderEntity.Address,
                CreatedDate = orderEntity.CreatedDate,
                UpdatedDate = orderEntity.UpdatedDate,
                Customer = orderEntity.Customer != null ? new CustomerBaseDTO
                {
                    Id = orderEntity.Customer.Id,
                    Name = orderEntity.Customer.Name,
                    Email = orderEntity.Customer.Email,
                    Phone = orderEntity.Customer.Phone,
                    CreatedDate = orderEntity.Customer.CreatedDate,
                    UpdatedDate = orderEntity.Customer.UpdatedDate
                } : null
            };
        }

        public async Task AddAsync(CreateOrderDTO orderDTO)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                Description = orderDTO.Description,
                Address = orderDTO.Address,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Products = new List<Product>(),
                CustomerId = orderDTO.CustomerId
            };

            await _orderWriteRepository.AddAsync(order);
            await _orderWriteRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, UpdateOrderDTO orderDTO)
        {
            var order = await _orderReadRepository.GetByIdAsync(id);

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            order.Description = orderDTO.Description;
            order.Address = orderDTO.Address;
            order.UpdatedDate = DateTime.UtcNow;

            await _orderWriteRepository.Update(order);
            await _orderWriteRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var order = await _orderReadRepository.GetByIdAsync(id);

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            await _orderWriteRepository.Delete(order);
            await _orderWriteRepository.SaveChangesAsync();
        }
    }
}
