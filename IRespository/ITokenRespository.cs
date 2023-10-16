using Microsoft.AspNetCore.Identity;

namespace Gym_Management.IRespository
{
    public interface ITokenRespository
    {
        string CreateToken(IdentityUser user, List<string> roles);
    }
}
