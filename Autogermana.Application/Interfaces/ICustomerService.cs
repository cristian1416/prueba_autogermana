using Autogermana.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autogermana.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResponseDTO> GetCustomerById(string customerId, CancellationToken ct);
    }
}
