using LNSF.API.Utils;
using LNSF.Application.Interfaces;
using LNSF.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace LNSF.API.ServiceFilters;

public class AuthAndUserExtractionFilter : IAsyncActionFilter
{
	private readonly IAuthService _authService;

	public AuthAndUserExtractionFilter(IAuthService authService) =>
		_authService = authService;

	public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
			throw new AppException("Token não encontrado!", HttpStatusCode.Unauthorized);

		var auth = context.HttpContext.Request.Headers.Authorization.ToString();
		auth = AuthUtil.ExtractTokenFromHeader(auth);

		(var userId, var userRoles) = _authService.GetUserIds(auth).Result;

		context.HttpContext.Items.Add("CurrentUserId", userId);
		context.HttpContext.Items.Add("CurrentUserRoles", userRoles);

		return next();
	}
}
