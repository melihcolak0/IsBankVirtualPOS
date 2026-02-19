namespace IsBankVirtualPOS.WebUI.DTOs.PaymentDTOs
{
    public class CreatePaymentDTO
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public int Provider { get; set; }
    }
}
