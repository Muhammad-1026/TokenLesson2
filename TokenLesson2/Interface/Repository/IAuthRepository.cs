using TokenLesson2.Models.User;

namespace TokenLesson2.Interface.Repository;

public interface IAuthRepository
{
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellation = default);
}
