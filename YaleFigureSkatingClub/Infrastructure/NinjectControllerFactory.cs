using System;
using System.Linq;
using System.Web.Mvc;
using System.Reflection;
using System.Web.Routing;
using Ninject;

namespace YaleFigureSkatingClub
{
	public class NinjectControllerFactory : IControllerFactory
	{
		public IController CreateController(RequestContext requestContext, string controllerName)
		{
			IContainerAccessor containerAccessor = requestContext.HttpContext.ApplicationInstance as IContainerAccessor;
			Assembly currentAssembly = Assembly.GetExecutingAssembly();
			var controllerTypes = currentAssembly.GetTypes().Where(t => t.Name.Contains(controllerName + "Controller"));
			                                                       
			if (controllerTypes.Any()) {
				return containerAccessor.NinjectKernel.Get(controllerTypes.First ()) as IController;
			}
			return null;
		}
		
		public void ReleaseController(IController controller)
		{
			controller = null;
		}
	}
}

