using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace YaleFigureSkatingClub.Infrastructure
{
	// Json Model binder - idea from http://stackoverflow.com/questions/4164114/posting-json-data-to-asp-net-mvc
	public class JsonModelBinder : DefaultModelBinder
	{
	
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) 
		{
			if (IsJsonRequest(controllerContext)) {
				return BindJsonModel(controllerContext, bindingContext);
			}
			
			return base.BindModel(controllerContext, bindingContext);
		}
		
		static object BindJsonModel(ControllerContext controllerContext, ModelBindingContext bindingContext) 
		{
			var request = controllerContext.HttpContext.Request;
			var jsonStringData = new StreamReader(request.InputStream).ReadToEnd();
			
			return new JavaScriptSerializer().Deserialize(jsonStringData, bindingContext.ModelType);
		}
		
		static bool IsJsonRequest(ControllerContext context)
		{
			var contentType = context.HttpContext.Request.ContentType;
			return contentType.Contains("application/json");
		}
	}
}

