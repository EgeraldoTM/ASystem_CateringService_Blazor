using System.Net;
using System.Text.Json;
using CateringApi.Helpers.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CateringApi.Web.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch(NotFoundException e)
			{
				context.Response.StatusCode = (int)HttpStatusCode.NotFound;

				ProblemDetails problem = new()
				{
					Status = (int)HttpStatusCode.NotFound,
					Type = "Not found",
					Title = "Not found",
					Detail = e.Message
				};

				var json = JsonSerializer.Serialize(problem);

				context.Response.ContentType = "application/json";

				await context.Response.WriteAsJsonAsync(json);
			}
			catch (Exception)
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				ProblemDetails problem = new()
				{
					Status = (int)HttpStatusCode.InternalServerError,
					Type = "Server error",
					Title = "Server error",
					Detail = "An internal server error occurred"
				};

				var json = JsonSerializer.Serialize(problem);

				context.Response.ContentType = "application/json";

				await context.Response.WriteAsJsonAsync(json);
			}
		}
	}
}
