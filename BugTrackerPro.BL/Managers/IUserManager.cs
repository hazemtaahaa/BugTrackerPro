using BugTrackerPro.BL.DTOs;
using System;
using System.Threading.Tasks;

namespace BugTrackerPro.BL.Managers;

public interface IUserManager
{
    Task<AuthResponseDto> RegisterAsync(UserRegisterDto dto);
    Task<AuthResponseDto> LoginAsync(UserLoginDto dto);
    Task<UserDto> GetUserByIdAsync(Guid id);
} 