using System.Threading.Tasks;
namespace BeautySalonWebApplication.Services
{
	public interface IViewRenderService
	{
		Task<string> RenderToStringAsync(string viewName);
	}
}
