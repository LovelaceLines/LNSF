using LNSF.API.InputModels;
using LNSF.Domain.DTOs;
using LNSF.Domain.Filters;
using LNSF.Test.DTOs;
using LNSF.Test.Fakers;
using LNSF.Test.Global;
using System.Net;

namespace LNSF.Test.API;

public class UserControllerTests : GlobalClientRequest
{
	[Fact]
	public async Task Get_Query_ReturnsOk()
	{
		var user = await GetUser();
		var role = await GetRole();
		var userRole = await GetUserRole(userId: user.Id, roleId: role.Id);
		var filter = new UserFilter
		{
			Id = user.Id,
			Name = user.Name,
			UserName = user.UserName,
			Email = user.Email,
			PhoneNumber = user.PhoneNumber
		};

		var result = await GetFromQuery<QueryResult<UserDTO>>(_userClient, filter);
		var item = result.Items.Single();

		Assert.Equal(user.Id, item.Id);
		Assert.Equal(user.Name, item.Name);
		Assert.Equal(user.UserName, item.UserName);
		Assert.Equal(user.Email, item.Email);
		Assert.Equal(user.PhoneNumber, item.PhoneNumber);
		Assert.Contains(role.Name, item.Roles.Select(r => r.Name));
	}

	[Fact]
	public async Task Put_ValidUser_ReturnsOkResult()
	{
		var user = await GetUser();
		var role = await GetRole();
		var userRole = await GetUserRole(userId: user.Id, roleId: role.Id);
		var token = await GetToken(userName: user.UserName, password: user.Password);
		_accessToken = token.AuthToken.AccessToken;
		var fake = new UserFake(id: user.Id).Generate();

		var result = await PutFromBody<UserDTO>(_userClient, fake);

		Assert.Equal(fake.Id, result.Id);
		Assert.Equal(fake.Name, result.Name);
		Assert.Equal(fake.UserName, result.UserName);
		Assert.Equal(fake.Email, result.Email);
		Assert.Equal(fake.PhoneNumber, result.PhoneNumber);
	}

	[Fact]
	public async Task Put_InvalidUserWithExistingUserNameEmailOrPhoneNumber_ReturnsConflictResult()
	{
		var role = await GetRole();
		var user1 = await GetUser();
		await GetUserRole(userId: user1.Id, roleId: role.Id);
		var user2 = await GetUser();
		await GetUserRole(userId: user2.Id, roleId: role.Id);
		var token = await GetToken(userName: user1.UserName, password: user1.Password);
		_accessToken = token.AuthToken.AccessToken;
		var userWithExistingUserName = new UserFake(id: user1.Id, userName: user2.UserName).Generate();
		var userWithExistingEmail = new UserFake(id: user1.Id, email: user2.Email).Generate();
		var userWithExistingPhoneNumber = new UserFake(id: user1.Id, phoneNumber: user2.PhoneNumber).Generate();

		var resultWithExistingUserName = await PutFromBody<AppHttpResponse>(_userClient, userWithExistingUserName);
		// var resultWithExistingEmail = await PutFromBody<AppHttpResponse>(_userClient, userWithExistingEmail);
		// var resultWithExistingPhoneNumber = await PutFromBody<AppHttpResponse>(_userClient, userWithExistingPhoneNumber);

		Assert.Equal(HttpStatusCode.Conflict, resultWithExistingUserName.StatusCode);
		// Assert.Equal(HttpStatusCode.Conflict, resultWithExistingEmail.StatusCode);
		// Assert.Equal(HttpStatusCode.Conflict, resultWithExistingPhoneNumber.StatusCode);
	}

	[Fact]
	public async Task Put_Password_ValidUser_ReturnsOkResult()
	{
		var user = await GetUser();
		var role = await GetRole();
		var userRole = await GetUserRole(userId: user.Id, roleId: role.Id);
		var token = await GetToken(userName: user.UserName, password: user.Password);
		_accessToken = token.AuthToken.AccessToken;
		var model = new UpdatePasswordIM { OldPassword = user.Password, NewPassword = new UserFake().Generate().Password };

		var result = await PutFromBody<UserDTO>(_userPasswordClient, model);

		Assert.Equal(user.Id, result.Id);
		Assert.Equal(user.Name, result.Name);
		Assert.Equal(user.UserName, result.UserName);
		Assert.Equal(user.Email, result.Email);
		Assert.Equal(user.PhoneNumber, result.PhoneNumber);
	}

	[Fact]
	public async Task Put_Password_InvalidOldPassword_ReturnsBadRequestResult()
	{
		var model = new UpdatePasswordIM { OldPassword = new UserFake().Generate().Password, NewPassword = new UserFake().Generate().Password };

		var result = await PutFromBody<AppHttpResponse>(_userPasswordClient, model);

		Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
	}

	[Fact]
	public async Task Delete_ValidId_ReturnsOkResult()
	{
		var user = await GetUser();

		var result = await DeleteFromUri<UserDTO>(_userClient, user.Id);

		Assert.Equal(user.Id, result.Id);
		Assert.Equal(user.Name, result.Name);
		Assert.Equal(user.UserName, result.UserName);
		Assert.Equal(user.Email, result.Email);
		Assert.Equal(user.PhoneNumber, result.PhoneNumber);
	}

	[Fact]
	public async Task Post_AddToRole_ValidUser_ReturnsOkResult()
	{
		var user = await GetUser();
		var role = await GetRole();
		var model = new UserRoleIM { UserId = user.Id, RoleId = role.Id };

		var result = await PostFromBody<AppHttpResponse>(_addUserToRoleClient, model);

		Assert.Equal(HttpStatusCode.OK, result.StatusCode);
	}

	[Fact]
	public async Task Post_AddToRole_InvalidUser_ReturnsNotFoundResult()
	{
		var role = await GetRole();
		var userRole = new { UserId = new Random().Next(1000, 9999), RoleName = role.Name };

		var result = await PostFromBody<AppHttpResponse>(_addUserToRoleClient, userRole);

		Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
	}

	[Fact]
	public async Task Post_AddToRole_InvalidRole_ReturnsNotFoundResult()
	{
		var user = await GetUser();
		var userRole = new { UserId = user.Id, RoleName = new RoleFake().Generate().Name };

		var result = await PostFromBody<AppHttpResponse>(_addUserToRoleClient, userRole);

		Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
	}

	[Fact]
	public async Task Post_AddToRole_RepeatedRole_ReturnsConflictResult()
	{
		var userRole = await GetUserRole();

		var result = await PostFromBody<AppHttpResponse>(_addUserToRoleClient, userRole);

		Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);
	}

	[Fact]
	public async Task Delete_RemoveFromRole_ValidUser_ReturnsOkResult()
	{
		var userRole = await GetUserRole();

		var result = await DeleteFromBody<AppHttpResponse>(_removeUserFromRoleClient, userRole);

		Assert.Equal(HttpStatusCode.OK, result.StatusCode);
	}

	[Fact]
	public async Task Delete_RemoveFromRole_InvalidUser_ReturnsBadRequestResult()
	{
		var role = await GetRole();
		var userRole = new UserRoleIM { UserId = new Random().Next(1000, 9999), RoleId = role.Id };

		var result = await DeleteFromBody<AppHttpResponse>(_removeUserFromRoleClient, userRole);

		Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
	}

	[Fact]
	public async Task Delete_RemoveFromRole_InvalidRole_ReturnsBadRequestResult()
	{
		var user = await GetUser();
		var userRole = new UserRoleIM { UserId = user.Id, RoleId = new Random().Next(1, 1000) };

		var result = await DeleteFromBody<AppHttpResponse>(_removeUserFromRoleClient, userRole);

		Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
	}

	[Fact]
	public async Task Delete_RemoveFromRole_NotInRole_ReturnsBadRequestResult()
	{
		var user = await GetUser();
		var role = await GetRole();

		var result = await DeleteFromBody<AppHttpResponse>(_removeUserFromRoleClient, new UserRoleIM { UserId = user.Id, RoleId = role.Id });

		Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
	}
}
