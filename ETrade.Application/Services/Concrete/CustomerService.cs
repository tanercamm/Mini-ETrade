using ETrade.Application.DTOs.Customer;
using ETrade.Application.DTOs.Order;
using ETrade.Application.Services.Abstract;
using ETrade.Domain.Entities;
using ETrade.Domain.Repositories.Customer;

namespace ETrade.Application.Services.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;

        public CustomerService(ICustomerReadRepository customerReadRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;
        }

        public async Task<List<CustomerBaseDTO>> GetAll()
        {
            // IQueryable olduğu için ToList() ile liste haline getiriyoruz.
            var customerEntities = _customerReadRepository.GetAll().ToList();

            return customerEntities.Select(customer => new CustomerBaseDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                CreatedDate = customer.CreatedDate,
                UpdatedDate = customer.UpdatedDate
            }).ToList();
        }

        public async Task<CustomerDTO> GetByIdAsync(string id)
        {
            var customerEntity = await _customerReadRepository.GetByIdAsync(id);

            if (customerEntity == null)
            {
                throw new Exception("Customer not found");
            }

            return new CustomerDTO
            {
                Id = customerEntity.Id,
                Name = customerEntity.Name,
                Email = customerEntity.Email,
                Phone = customerEntity.Phone,
                CreatedDate = customerEntity.CreatedDate,
                UpdatedDate = customerEntity.UpdatedDate,
                Orders = customerEntity.Orders?.Select(order => new OrderBaseDTO
                {
                    Id = order.Id,
                    Description = order.Description,
                    Address = order.Address,
                    CreatedDate = order.CreatedDate,
                    UpdatedDate = order.UpdatedDate

                }).ToList()
            };
        }

        public async Task AddAsync(CreateCustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = customerDTO.Name,
                Email = customerDTO.Email,
                Phone = customerDTO.Phone,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Orders = new List<Order>()
            };

            await _customerWriteRepository.AddAsync(customer);
            await _customerWriteRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateCustomerDTO customerDTO)
        {
            var customer = await _customerReadRepository.GetByIdAsync(customerDTO.Id.ToString());

            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            customer.Name = customerDTO.Name;
            customer.Email = customerDTO.Email;
            customer.Phone = customerDTO.Phone;
            customer.UpdatedDate = DateTime.UtcNow;

            await _customerWriteRepository.Update(customer);
            await _customerWriteRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var customer = await _customerReadRepository.GetByIdAsync(id);

            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            await _customerWriteRepository.Delete(customer);
            await _customerWriteRepository.SaveChangesAsync();
        }
    }
}
