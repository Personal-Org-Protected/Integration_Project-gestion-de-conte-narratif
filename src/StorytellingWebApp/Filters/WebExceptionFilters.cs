using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace StorytellingWebApp.Filters
{
    public class WebExceptionFilters : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine(context.Exception.Message);
            Console.WriteLine(context.Exception.Data);
            Console.WriteLine(context.Exception.InnerException);
            HandleException(context);
            context.ExceptionHandled = true;
        }

        private void HandleException(ExceptionContext context)
        {
                var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState);

            //here we go
            viewData.Add("id", context.Exception.HResult);
            viewData.Add("Message", context.Exception.Message);
                var res = new ViewResult()
                {
                    ViewName = "Error",
                    ViewData = viewData,
                };
                context.Result = res;
            context.ExceptionHandled = true;
        }

    }
}
