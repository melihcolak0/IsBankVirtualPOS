namespace IsBankVirtualPOS.WebUI.DTOs.PaymentDTOs
{
    public class PaymentSummaryDTO
    {
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = default!;
        public bool CanRefund { get; set; }
        public int AttemptCount { get; set; }
    }
}
