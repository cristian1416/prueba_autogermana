using Autogermana.Application.DTO;
using Autogermana.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autogermana.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IPowerAutomateRepository _powerAutomateRepository;
        public CustomerService(IPowerAutomateRepository powerAutomateRepository)
        {
            _powerAutomateRepository = powerAutomateRepository;
        }

        public async Task<CustomerResponseDTO> GetCustomerById(string customerId, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                throw new ArgumentException("CustomerId es obligatorio.", nameof(customerId));

            var entity = await _powerAutomateRepository.GetCustomerEntity(customerId, ct);

            return new CustomerResponseDTO
            {
                firstname = entity.Firstname,
                lastname = entity.LastName,
                age = entity.Age,
                city = entity.City,
                email = entity.Email,
            };

        }
    }
}
