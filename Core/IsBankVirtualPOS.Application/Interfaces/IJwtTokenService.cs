using IsBankVirtualPOS.Domain.Entities;

namespace IsBankVirtualPOS.Application.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> CreateTokenAsync(AppUser user);
    }
}
