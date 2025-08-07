using TokenLesson2.Dtos.Request;
using TokenLesson2.Dtos.Response;

namespace TokenLesson2.Interface.Services;

public interface IAuthService
{
    Task<UserDto?> LoginAsync(LoginRequestDto loginRequestDto, CancellationToken cancellation = default);
}
