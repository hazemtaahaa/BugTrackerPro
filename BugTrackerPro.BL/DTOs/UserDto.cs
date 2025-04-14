using System;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerPro.BL.DTOs;

public class UserRegisterDto
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}

public class UserLoginDto
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Password { get; set; }
}

public class UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}

public class AuthResponseDto
{
    public string Token { get; set; }
    public UserDto User { get; set; }
} 