using System.ComponentModel.DataAnnotations;

namespace IsBankVirtualPOS.WebUI.DTOs.PaymentDTOs
{
    public class StartPaymentDTO
    {
        [Required]
        public Guid PaymentId { get; set; }
        [Required]
        public string CardHolder { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public string ExpireMonth { get; set; }
        [Required]
        public string ExpireYear { get; set; }
        [Required]
        public string CVV { get; set; }

        public bool Use3D { get; set; }
    }
}
