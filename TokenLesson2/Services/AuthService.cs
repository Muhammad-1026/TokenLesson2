using AutoMapper;
using TokenLesson2.Common.Mappings;
using TokenLesson2.Dtos.Request;
using TokenLesson2.Dtos.Response;
using TokenLesson2.Interface.Repository;
using TokenLesson2.Interface.Services;

namespace TokenLesson2.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public AuthService(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<UserDto?> LoginAsync(LoginRequestDto loginRequestDto, CancellationToken cancellation = default)
        {
            var userName = loginRequestDto.UserName;
            var userPassword = loginRequestDto.UserPassword;

            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("UserName не может быт null");

            if (string.IsNullOrWhiteSpace(userPassword))
                throw new ArgumentException("UserPassword не может быт null");

            var user = await _authRepository.GetByUserNameAsync(userName, cancellation);

            if (user is null || !BCrypt.Net.BCrypt.Verify(userPassword, user.UserPassword))
                throw new UnauthorizedAccessException("Неправильное имя пользователя или пароль");

            return _mapper.Map<UserDto>(user);
        }
    }
}