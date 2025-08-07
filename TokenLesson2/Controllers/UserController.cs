using Microsoft.AspNetCore.Mvc;
using TokenLesson2.Dtos.Request;
using TokenLesson2.Dtos.Response;
using TokenLesson2.Interface.Services;

namespace TokenLesson2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public UserController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll(CancellationToken cancellationToken = default)
    {
        return Ok(await _userService.GetAllAsync(cancellationToken));
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createUserDto, CancellationToken cancellationToken = default)
    {
        return await _userService.CreateUserAsync(createUserDto, cancellationToken);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto?>> Login(LoginRequestDto loginRequestDto, CancellationToken cancellationToken = default)
    {
        return await _authService.LoginAsync(loginRequestDto, cancellationToken);
    }

    [HttpPut]
    public async Task<ActionResult<UserDto>> UpdateUser(UpdateUserDto updateUserDto, CancellationToken cancellationToken = default)
    {
        return await _userService.UpdateUserAsync(updateUserDto, cancellationToken);
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Ok(true);
    }
}