using Microsoft.AspNetCore.Identity;

namespace MyFirstApi.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
