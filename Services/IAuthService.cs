using Repleet.Contracts;
using Repleet.Data;
using Repleet.Models.Entities;

namespace Repleet.Services
{
    public interface IAuthService
    {
        Task<TokenResponseDTO> LoginAsync(ApplicationUserDTO request);
        Task<ApplicationUser?> RegisterAsync(ApplicationUserDTO request);
        Task<TokenResponseDTO?> RefreshTokensAsync(RefreshTokenRequestDTO dto);

        
    }
}
