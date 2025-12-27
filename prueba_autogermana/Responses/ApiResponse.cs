using Autogermana.Application.DTO;

namespace Autogermana.Api.Responses
{
    public class ApiResponse<T> : BaseResponseDTO
    {
        public T response {  get; set; }

        public ApiResponse(T _response, string _time, bool _result, int _status, string? _errors)
        {
            response = _response;
            time = _time;
            result = _result;
            status = _status;   
            errors = _errors;
        }


    }
}
