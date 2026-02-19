using IsBankVirtualPOS.WebUI.DTOs.PaymentDTOs;

namespace IsBankVirtualPOS.WebUI.Services.PaymentServices
{
    public class PaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;

        public PaymentService(IHttpClientFactory factory, IHttpContextAccessor contextAccessor)
        {
            _httpClient = factory.CreateClient("ApiClient");
            _contextAccessor = contextAccessor;
        }

        private void AddToken()
        {
            var token = _contextAccessor.HttpContext?
                .User.Claims.FirstOrDefault(x => x.Type == "AccessToken")?.Value;

            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<Guid?> CreatePaymentAsync(CreatePaymentDTO dto)
        {
            AddToken();

            var response = await _httpClient.PostAsJsonAsync("/api/payments", dto);

            if (!response.IsSuccessStatusCode)
                return null;

            var result =
                await response.Content.ReadFromJsonAsync<CreatePaymentResponseDTO>();

            return result?.PaymentId;
        }

        public async Task<string?> StartPayment(StartPaymentDTO dto)
        {
            AddToken();

            var response = await _httpClient.PostAsJsonAsync(
                "/api/payments/start",
                dto);

            if (!response.IsSuccessStatusCode)
                return null;

            if (response.Content.Headers.ContentType?.MediaType == "text/html")
                return await response.Content.ReadAsStringAsync();

            return null;
        } 

        public async Task<PaymentSummaryDTO?> GetSummary(Guid paymentId)
        {
            AddToken();

            return await _httpClient
                .GetFromJsonAsync<PaymentSummaryDTO>(
                    $"/api/payments/{paymentId}/summary");
        }

        public async Task Refund(Guid paymentId)
        {
            AddToken();

            var summary = await GetSummary(paymentId);

            if (summary == null)
                return;

            await _httpClient.PostAsJsonAsync(
                "/api/refunds/refund",
                new
                {
                    paymentId = paymentId,
                    amount = summary.Amount,
                    reason = "UI refund"
                });
        }

        public async Task<ThreeDPaymentFormDTO?> Start3D(StartPaymentDTO dto)
        {
            AddToken();

            var response = await _httpClient.PostAsJsonAsync(
                "/api/payments/start-3d",
                dto);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content
                .ReadFromJsonAsync<ThreeDPaymentFormDTO>();
        }
    }
}
