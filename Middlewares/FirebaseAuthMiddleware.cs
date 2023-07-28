using System;
using FirebaseAdmin.Auth;

namespace OzMateApi.Middlewares
{
	public class FirebaseAuthMiddleware
	{
        private readonly RequestDelegate _next;

        public FirebaseAuthMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var authHeader = context.Request.Headers["Authorization"].ToString();
			if(authHeader.StartsWith("Bearer "))
			{
				var idToken = authHeader.Substring("Bearer ".Length);
				try
				{
					var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
					await _next(context);

				}
				catch (FirebaseAuthException)
				{
					context.Response.StatusCode = StatusCodes.Status401Unauthorized;
					return;
				}
			}
			else
			{
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
		}
	}
}

