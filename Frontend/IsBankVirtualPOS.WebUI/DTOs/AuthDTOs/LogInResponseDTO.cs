namespace IsBankVirtualPOS.WebUI.DTOs.AuthDTOs
{
    public class LogInResponseDTO
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public Guid UserId { get; set; }
    }
}
