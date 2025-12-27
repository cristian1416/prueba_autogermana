using Autogermana.Application.DTO;
using Autogermana.Application.Interfaces;
using Autogermana.Domain.Entities;
using Autogermana.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Autogermana.Infrastructure.Repository
{
    public sealed class PowerAutomateRepository : IPowerAutomateRepository
    {
        private readonly HttpClient _client;
        private readonly PAOptions _options;
        private readonly ITokenService _tokenService;
        public PowerAutomateRepository(HttpClient client, ITokenService tokenService, IOptions<PAOptions> options)
        {
            _client = client;
            _tokenService = tokenService;
            _options = options.Value;
        }

        public async Task<CustomerEntity> GetCustomerEntity(string customerId, CancellationToken ct)
        {
           var token = await _tokenService.GetToken(ct);

            using var cons = new HttpRequestMessage(HttpMethod.Post, _options.UrlCustomer);

            cons.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            cons.Content = JsonContent.Create(new { CustomerId = customerId });

            try
            {
                using var res = await _client.SendAsync(cons, ct);

                if (res.StatusCode == HttpStatusCode.NotFound) 
                {
                    throw new KeyNotFoundException("Cliente no encontrado");
                }

                if (!res.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error status = {(int)res.StatusCode}");
                }

                var dto = await res.Content.ReadFromJsonAsync<CustomerResponseDTO>(ct);

                if (dto is null)
                {
                    throw new HttpRequestException("Respuesta vacia");
                }

                return new CustomerEntity(
                    firstname: dto.firstname,
                    lastName:dto.lastname,
                    email:dto.email,
                    age:dto.age,
                    city: dto.city
                    );
            }
            catch (TaskCanceledException ex) when (!ct.IsCancellationRequested)
            {

                throw new TimeoutException("terminado por timeout", ex);
            }
        }
    }
}
