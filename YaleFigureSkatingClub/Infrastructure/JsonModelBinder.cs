using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.Web.Mvc.DataAnnotations;

namespace YaleFigureSkatingClub.Infrastructure
{
	// Json Model binder - idea from http://stackoverflow.com/questions/4164114/posting-json-data-to-asp-net-mvc
	public class JsonModelBinder : DataAnnotationsModelBinder
	{
	
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) 
		{
			if (IsJsonRequest(controllerContext)) {
                return BindJsonModel(controllerContext, bindingContext);
			}
			
			return base.BindModel(controllerContext, bindingContext);
		}
		
		object BindJsonModel(ControllerContext controllerContext, ModelBindingContext bindingContext) 
		{
			var request = controllerContext.HttpContext.Request;
			var jsonStringData = new StreamReader(request.InputStream).ReadToEnd();
			var model = new JavaScriptSerializer().Deserialize(jsonStringData, bindingContext.ModelType);

            ValidateModel(model, bindingContext);

			return model;
		}
		
		void ValidateModel(object model, ModelBindingContext bindingContext, List<int> validatedObjects = null)
		{
			validatedObjects = validatedObjects ?? new List<int>();
			
			var typeDescriptor = GetTypeDescriptor(model, model.GetType ());
			
			// validate the model
			foreach (ValidationAttribute attribute in typeDescriptor.GetAttributes().OfType<ValidationAttribute>()) {
				if (!attribute.IsValid (bindingContext.Model)) {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, attribute.FormatErrorMessage(bindingContext.ModelName));
                }
				validatedObjects.Add (model.GetHashCode());
            }
			
			// validate properties, recurse if necessary
			foreach (PropertyDescriptor property in typeDescriptor.GetProperties()) {
				var propInstance = property.GetValue (model);

                if (propInstance == null) {
                    foreach (ValidationAttribute attribute in property.Attributes.OfType<ValidationAttribute>()) {
                        if (!attribute.IsValid(propInstance)) {
                            bindingContext.ModelState.AddModelError(property.Name, attribute.FormatErrorMessage(property.Name));
                            if (propInstance != null) {
                                validatedObjects.Add(propInstance.GetHashCode());
                            }
                        }
                        else if (propInstance != null) {
                            ValidateModel(propInstance, bindingContext, validatedObjects);
                        }
                    }
                } 
			}
		}
			 
		
		static ICustomTypeDescriptor GetTypeDescriptor(object model, Type modelType) 
        {
            var provider = new AssociatedMetadataTypeTypeDescriptionProvider(modelType);
            return provider.GetTypeDescriptor(modelType, model);
        }

		static bool IsJsonRequest(ControllerContext context)
		{
			var contentType = context.HttpContext.Request.ContentType;
			return contentType.Contains("application/json");
		}
	}
}

