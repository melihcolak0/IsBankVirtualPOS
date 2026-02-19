using IsBankVirtualPOS.WebUI.DTOs.OrderDTOs;

namespace IsBankVirtualPOS.WebUI.Services.OrderServices
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        public async Task<Guid?> CreateOrder(CreateOrderDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/orders", dto);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<OrderResponseDTO>();

            return result?.OrderId;
        }
    }
}
