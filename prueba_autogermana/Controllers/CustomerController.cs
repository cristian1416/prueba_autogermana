using Autogermana.Api.Responses;
using Autogermana.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Autogermana.Api.Controllers
{
    [Route("api/v1/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService) 
        {
            _customerService = customerService;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerId([FromRoute]string customerId, CancellationToken ct)
        {
            var sw = Stopwatch.StartNew();

            var dto = await _customerService.GetCustomerById(customerId, ct);

            sw.Stop();

            var res = new ApiResponse<object>(
                _response: dto,
                _time: $"{sw.ElapsedMilliseconds} ms",
                _result: true,
                _status: StatusCodes.Status200OK,
                _errors:null
                );

            return StatusCode(StatusCodes.Status200OK, res);
        }
    }
}
