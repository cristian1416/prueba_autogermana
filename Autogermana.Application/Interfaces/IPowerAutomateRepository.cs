using Autogermana.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autogermana.Application.Interfaces
{
    public interface IPowerAutomateRepository
    {
        Task<CustomerEntity> GetCustomerEntity(string customerId, CancellationToken ct);
    }
}
