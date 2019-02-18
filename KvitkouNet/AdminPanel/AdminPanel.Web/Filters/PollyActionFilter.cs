using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Rest;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Web.Filters
{
	public class PollyActionFilter : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var policy = Policy.Handle<HttpOperationException>()
				.WaitAndRetryAsync(new[]
				{
					TimeSpan.FromSeconds(1),
					TimeSpan.FromSeconds(1),
					TimeSpan.FromSeconds(1)
				});

			await policy.ExecuteAsync(async () => await next());
		}
	}
}
