using ETrade.Application.DTOs.Order;

namespace ETrade.Application.Services.Abstract
{
    public interface IOrderService
    {
        Task<List<OrderBaseDTO>> GetAll();

        Task<OrderDTO> GetByIdAsync(string id);

        Task AddAsync(CreateOrderDTO orderDTO);

        Task UpdateAsync(UpdateOrderDTO orderDTO);

        Task DeleteAsync(string id);
    }
}
