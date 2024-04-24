using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BeautySalonWebApplication.Services
{
	public class ViewRenderService(IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider) : IViewRenderService
	{
		private readonly IRazorViewEngine _razorViewEngine = razorViewEngine;
		private readonly ITempDataProvider _tempDataProvider = tempDataProvider;
		private readonly IServiceProvider _serviceProvider = serviceProvider;

		public async Task<string> RenderToStringAsync(string viewName)
		{
			var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
			var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

			using var sw = new StringWriter();
			var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);

			if (viewResult.View == null)
			{
				throw new ArgumentNullException($"{viewName} does not match any available view");
			}

			var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());

			var viewContext = new ViewContext(
				actionContext,
				viewResult.View,
				viewDictionary,
				new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
				sw,
				new HtmlHelperOptions());

			var view = (RazorPage)viewResult.View;
			view.ViewContext = viewContext;

			await view.ExecuteAsync();
			return sw.ToString();
		}
	}
}
