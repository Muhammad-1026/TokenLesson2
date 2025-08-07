using Microsoft.EntityFrameworkCore;
using TokenLesson2.DataContext;
using TokenLesson2.Interface.Repository;
using TokenLesson2.Models.User;

namespace TokenLesson2.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AplicationDbContext _context;

    public AuthRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);

        return user;
    }
}















//public class LoginDto
//{
//    public string Token { get; set; }
//    public UserDto User { get; set; }
//}

//private string RoleToString(int role)
//{
//    return role switch
//    {
//        1 => "Admin",
//        2 => "User",
//    };
//}