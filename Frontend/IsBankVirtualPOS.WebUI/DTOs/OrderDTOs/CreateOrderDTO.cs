namespace IsBankVirtualPOS.WebUI.DTOs.OrderDTOs
{
    public class CreateOrderDTO
    {
        public Guid AppUserId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "TRY";
    }
}
