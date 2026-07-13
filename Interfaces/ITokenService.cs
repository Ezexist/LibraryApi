using Library.Models;

namespace Library.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
