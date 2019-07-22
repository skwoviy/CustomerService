using CustomerService.Resources;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace CustomerService.Attributes
{
    public class ValidateResourceParametersAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor != null)
            {
                var routeItemsCount = descriptor.RouteValues.Count();
                var arguments = context.HttpContext.Request.Path.Value.TrimStart('/').Split('/');
                var argumentsToCheck = arguments.Skip(routeItemsCount).ToArray();
                var items = context.HttpContext.Request.Query.ToDictionary(t => t.Key, t => t.Value);

                for (int i = 0; i < argumentsToCheck.Count(); i++)
                {
                    if (string.IsNullOrEmpty(argumentsToCheck[i]))
                    {
                        context.ModelState.AddModelError(string.Empty, Errors.noInquiryCriteria);
                        return;
                    }

                    StringValues item;
                    items.TryGetValue(argumentsToCheck[i], out item);

                    try
                    {
                        JsonConvert.DeserializeObject(item);
                    }
                    catch
                    {
                        context.ModelState.AddModelError("id", Errors.invalidCustomerId);
                        return;
                    }
                }
            }
        }
    }
}
