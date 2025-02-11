using ETrade.Application.DTOs.Customer;

namespace ETrade.Application.Services.Abstract
{
    public interface ICustomerService
    {
        Task<List<CustomerBaseDTO>> GetAll();

        Task<CustomerDTO> GetByIdAsync(string id);

        Task AddAsync(CreateCustomerDTO customerDTO);

        Task UpdateAsync(UpdateCustomerDTO customerDTO);

        Task DeleteAsync(string id);
    }
}
