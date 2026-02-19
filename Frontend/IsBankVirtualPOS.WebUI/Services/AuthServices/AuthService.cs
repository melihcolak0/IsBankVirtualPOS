using IsBankVirtualPOS.WebUI.DTOs.AuthDTOs;
using System.Net.Http;

namespace IsBankVirtualPOS.WebUI.Services.AuthServices
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        public async Task<bool> RegisterAsync(RegisterDTO dto)
        {
            var response =
                await _httpClient.PostAsJsonAsync("/api/auth/register", dto);

            return response.IsSuccessStatusCode;
        }

        public async Task<LogInResponseDTO?> LoginAsync(LogInDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", dto);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<LogInResponseDTO>();
        }
    }
}
