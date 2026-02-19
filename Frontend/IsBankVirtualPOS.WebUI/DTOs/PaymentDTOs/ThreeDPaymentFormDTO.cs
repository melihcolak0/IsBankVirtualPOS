namespace IsBankVirtualPOS.WebUI.DTOs.PaymentDTOs
{
    public class ThreeDPaymentFormDTO
    {
        public string CallbackUrl { get; set; } = default!;
        public Guid PaymentId { get; set; }
        public string MdStatus { get; set; } = default!;
        public string ResponseCode { get; set; } = default!;
        public string AuthCode { get; set; } = default!;
        public string BankReferenceNumber { get; set; } = default!;

        public decimal Amount { get; set; }
    }
}
