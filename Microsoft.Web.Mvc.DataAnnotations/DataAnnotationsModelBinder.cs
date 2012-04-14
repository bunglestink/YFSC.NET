namespace Microsoft.Web.Mvc.DataAnnotations {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.Web.Mvc.DataAnnotations.Properties;

    /// <summary>
    /// An implementation of <see cref="IModelBinder"/> which is designed to be a replacement
    /// default model binder, adding the functionality of validation via the validation
    /// attributes in System.ComponentModel.DataAnnotations from .NET 4.0.
    /// </summary>
    public class DataAnnotationsModelBinder : DefaultModelBinder {
        private Func<PropertyDescriptor, string, string, string> invalidValueFormatter;
        private static Func<PropertyDescriptor, string, string, string> defaultInvalidValueFormatter =
            (propDescriptor, value, displayName) => String.Format(Resources.InvalidValueForField, value, displayName);

        /// <summary>
        /// Gets or sets the invalid value formatter. Allows the user to determine how to format the
        /// display message when an invalid value is encountered (for example, entering "ABC" in a field
        /// which holds an Int32). The formatter is passed the property descriptor, the attempted value
        /// and the display name for the field, and is expected to return an error message.
        /// </summary>
        public Func<PropertyDescriptor, string, string, string> InvalidValueFormatter {
            get { return invalidValueFormatter ?? defaultInvalidValueFormatter; }
            set { invalidValueFormatter = value; }
        }

        /// <inheritdoc/>
        protected override PropertyDescriptorCollection GetModelProperties(ControllerContext controllerContext,
                                                                           ModelBindingContext bindingContext) {
            var properties = GetTypeDescriptor(bindingContext.Model, bindingContext.ModelType).GetProperties().Cast<PropertyDescriptor>();
            var filteredProperties = properties.Where(pd => ShouldUpdateProperty(pd, bindingContext.PropertyFilter));
            return new PropertyDescriptorCollection(filteredProperties.ToArray());
        }

        /// <inheritdoc/>
        protected override void BindProperty(ControllerContext controllerContext,
                                             ModelBindingContext bindingContext,
                                             PropertyDescriptor propertyDescriptor) {
            string fullPropertyKey = CreateSubPropertyName(bindingContext.ModelName, propertyDescriptor.Name);

            // Only bind properties that are part of the request
            if (bindingContext.ValueProvider.DoesAnyKeyHavePrefix(fullPropertyKey)) {
                var innerContext = new ModelBindingContext() {
                    Model = propertyDescriptor.GetValue(bindingContext.Model),
                    ModelName = fullPropertyKey,
                    ModelState = bindingContext.ModelState,
                    ModelType = propertyDescriptor.PropertyType,
                    ValueProvider = bindingContext.ValueProvider
                };
				
                IModelBinder binder = Binders.GetBinder(propertyDescriptor.PropertyType);
                object newPropertyValue = ConvertValue(propertyDescriptor, binder.BindModel(controllerContext, innerContext));
                ModelState modelState = bindingContext.ModelState[fullPropertyKey];

                // Only validate and bind if the property itself has no errors
                if (modelState.Errors.Count == 0) {
                    if (OnPropertyValidating(controllerContext, bindingContext, propertyDescriptor, newPropertyValue)) {
                        SetProperty(controllerContext, bindingContext, propertyDescriptor, newPropertyValue);
                        OnPropertyValidated(controllerContext, bindingContext, propertyDescriptor, newPropertyValue);
                    }
                }

                // There was an error getting the value from the binder, which was probably a format
                // exception (meaning, the data wasn't appropriate for the field)
                else {
                    foreach (var error in modelState.Errors.Where(err => err.ErrorMessage == "" && err.Exception != null).ToList()) {
                        for (var exception = error.Exception; exception != null; exception = exception.InnerException) {
                            if (exception is FormatException) {
                                string displayName = GetDisplayName(propertyDescriptor);
                                string errorMessage = InvalidValueFormatter(propertyDescriptor, modelState.Value.AttemptedValue, displayName);
                                modelState.Errors.Remove(error);
                                modelState.Errors.Add(errorMessage);
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <inheritdoc/>
        protected override void OnModelUpdated(ControllerContext controllerContext,
                                               ModelBindingContext bindingContext) {
			// Base version calls IDataErrorInfo
            base.OnModelUpdated(controllerContext, bindingContext);

            // If the model is invalid, don't run model-level validation rules
            if (!ModelIsValid(bindingContext)) {
                return;
            }
			
			var typeDescriptor = GetTypeDescriptor(bindingContext.Model, bindingContext.ModelType);
			
            foreach (ValidationAttribute attribute in typeDescriptor.GetAttributes<ValidationAttribute>()) {
				if (!attribute.IsValid (bindingContext.Model)) {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, attribute.FormatErrorMessage(bindingContext.ModelName));
                }
            }
        }

        /// <inheritdoc/>
        protected override bool OnPropertyValidating(ControllerContext controllerContext,
                                                     ModelBindingContext bindingContext,
                                                     PropertyDescriptor propertyDescriptor,
                                                     object value) {
            string modelStateKey = CreateSubPropertyName(bindingContext.ModelName, propertyDescriptor.Name);

            bool result = true;

            foreach (ValidationAttribute attribute in GetValidationAttributes(propertyDescriptor)) {
                if (!attribute.IsValid (bindingContext.Model)) {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, attribute.FormatErrorMessage(bindingContext.ModelName));
					result = false;
                }
            }

            return result;
        }

        // Helpers

        private static bool CanUpdateReadonlyTypedReference(Type type) {
            return !(type.IsValueType || type.IsArray || type == typeof(string));
        }

        /// <summary>
        /// Converts a data value. Looks for the <see cref="DisplayFormatAttribute"/> to determine
        /// if empty strings should be converted to nulls (true by default).
        /// </summary>
        /// <param name="propertyDescriptor">The property's property descriptor</param>
        /// <param name="value">The original value</param>
        /// <returns>The converted value</returns>
        internal static object ConvertValue(PropertyDescriptor propertyDescriptor,
                                            object value) {
            var displayFormatAttribute = propertyDescriptor.GetAttribute<DisplayFormatAttribute>();
            if ((displayFormatAttribute == null || displayFormatAttribute.ConvertEmptyStringToNull) && Object.Equals(value, String.Empty)) {
                return null;
            }

            return value;
        }

        /// <summary>
        /// Gets the display name for a class. Looks for the <see cref="DisplayNameAttribute"/>
        /// attribute, and falls back to the class's name by default.
        /// </summary>
        /// <param name="descriptor">The class's type descriptor</param>
        /// <returns>The display name of the class</returns>
        internal static string GetDisplayName(ICustomTypeDescriptor descriptor) {
            var displayNameAttribute = descriptor.GetAttribute<DisplayNameAttribute>();
            if (displayNameAttribute != null && !String.IsNullOrEmpty(displayNameAttribute.DisplayName)) {
                return displayNameAttribute.DisplayName;
            }

            return descriptor.GetClassName().Split('.', '+').Last();
        }

        /// <summary>
        /// Gets the display name for a property. Looks for the <see cref="DisplayAttribute"/>
        /// attribute, then the <see cref="DisplayNameAttribute"/> attribute, and finally falls
        /// back to the property's name by default.
        /// </summary>
        /// <param name="descriptor">The property's property descriptor</param>
        /// <returns>The display name of the property</returns>
        internal static string GetDisplayName(PropertyDescriptor descriptor) {

            var displayNameAttribute = descriptor.GetAttribute<DisplayNameAttribute>();
            if (displayNameAttribute != null && !String.IsNullOrEmpty(displayNameAttribute.DisplayName)) {
                return displayNameAttribute.DisplayName;
            }

            return descriptor.Name;
        }

        /// <summary>
        /// Gets the model's type descriptor. In order to support the buddy class metadata model
        /// for LINQ to SQL and Entity Framework, it uses
        /// <see cref="AssociatedMetadataTypeTypeDescriptionProvider"/>.
        /// </summary>
        /// <param name="model">The model object</param>
        /// <param name="modelType">The type of the model object</param>
        /// <returns>The model's type descriptor</returns>
        private static ICustomTypeDescriptor GetTypeDescriptor(object model, Type modelType) {
            var provider = new AssociatedMetadataTypeTypeDescriptionProvider(modelType);
            return provider.GetTypeDescriptor(modelType, model);
        }

        internal static IEnumerable<ValidationAttribute> GetValidationAttributes(PropertyDescriptor propertyDescriptor) {
            IEnumerable<ValidationAttribute> attributes = propertyDescriptor.GetAttributes<ValidationAttribute>();

            // Add an implied RequiredAttribute to non-nullable types if it's not already there
            if (!propertyDescriptor.PropertyType.IsNullable()) {
                if (!attributes.Any(attr => attr is RequiredAttribute)) {
                    attributes = attributes.Concat(new[] { new RequiredAttribute() });
                }
            }

            return attributes;
        }

        private static bool ModelIsValid(ModelBindingContext bindingContext) {
            if (String.IsNullOrEmpty(bindingContext.ModelName)) {
                return bindingContext.ModelState.IsValid;
            }

            return bindingContext.ModelState.IsValidField(bindingContext.ModelName);
        }

        private static bool ShouldUpdateProperty(PropertyDescriptor property, Predicate<string> propertyFilter) {
            if (property.IsReadOnly && !CanUpdateReadonlyTypedReference(property.PropertyType)) {
                return false;
            }

            return propertyFilter(property.Name);
        }

        private static bool TypeAllowsNullValue(Type type) {
            return (type.IsNullable());
        }
    }
}
