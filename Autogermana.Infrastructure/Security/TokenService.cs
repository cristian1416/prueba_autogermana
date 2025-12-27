using Autogermana.Application.Interfaces;
using Autogermana.Infrastructure.DTO;
using Autogermana.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Autogermana.Infrastructure.Security
{
    public class TokenService : ITokenService
    {
        private readonly HttpClient _httpClient;
        private readonly TokenOptions _options;
        private readonly IProtectionSecret _protection;

        private string? _cacheToken;
        private DateTimeOffset? _expires;

        public TokenService(HttpClient httpClient, IOptions<TokenOptions> options, IProtectionSecret protection)
        {
            _httpClient = httpClient;
            _protection = protection;
            _options = options.Value;
        }
        public async Task<string> GetToken(CancellationToken ct)
        {

            if (_options.dev)
            {
                return _options.TokenDev;
            }

            if (!string.IsNullOrWhiteSpace(_cacheToken) && DateTimeOffset.UtcNow < _expires.Value.AddMinutes(-1)) 
            {
                return _cacheToken;
            }

            var urlToken = _options.TokenUrl.Replace("{tenantId}", _options.TenantId.Trim());

            var clientSecret = _protection.Unprotect(_options.ClientSecret);

            var form = new Dictionary<string, string> 
            {
                ["client_id"] = _options.ClientId,
                ["client_secret"] = clientSecret,
                ["grant_type"] = "client_credentials",
            };

            using var res = new HttpRequestMessage(HttpMethod.Post, urlToken)
            {
                Content = new FormUrlEncodedContent(form)
            };

            using var cons = await _httpClient.SendAsync(res);

            var body = await cons.Content.ReadFromJsonAsync<TokenResponseDTO>(cancellationToken: ct);

            if (!cons.IsSuccessStatusCode || body?.access_token is null || body?.expires_in <= 0) 
            {
                throw new HttpRequestException($"No se pudo obtener el token status = {(int)cons.StatusCode}");

            }

            _cacheToken = body?.access_token;
            _expires = DateTimeOffset.UtcNow.AddSeconds(body.expires_in);


            return _cacheToken;
        }
    }
}
