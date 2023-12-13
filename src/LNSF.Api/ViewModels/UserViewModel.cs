﻿namespace LNSF.Api.ViewModels;

public class UserPostViewModel
{
    public string UserName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string Password { get; set; } = "";
}

public class UserViewModel
{
    public string Id { get; set; } = "";
    public string UserName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
}

public class UserGetViewModel
{
    public string Id { get; set; } = "";
    public string UserName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public List<string> Roles { get; set; } = new();
}

public class UserPutPasswordViewModel
{
    public string Id { get; set; } = "";
    public string OldPassword { get; set; } = "";
    public string NewPassword { get; set; } = "";
}

public class UserLoginViewModel
{
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
}
