using TokenLesson2.Dtos.Response;
using TokenLesson2.Models.User;

namespace TokenLesson2.Interface.Services;

public interface IJwtService
{
    TokenDto GenerateToken(User user);
    string GenerateRefreshToken();
}
