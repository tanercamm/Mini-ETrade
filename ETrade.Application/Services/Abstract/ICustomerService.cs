using ETrade.Application.DTOs.Customer;

namespace ETrade.Application.Services.Abstract
{
    public interface ICustomerService
    {
        Task<List<CustomerBaseDTO>> GetAll();

        Task<CustomerDTO> GetByIdAsync(string id);

        Task AddAsync(CreateCustomerDTO customerDTO);

        Task UpdateAsync(string id, UpdateCustomerDTO customerDTO);

        Task DeleteAsync(string id);
    }
}
