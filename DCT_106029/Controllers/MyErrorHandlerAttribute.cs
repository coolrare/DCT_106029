using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace DCT_106029.Controllers
{
    public class MyErrorHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, new
            {
                ErrorCode = 999,
                ErrorMsg = actionExecutedContext.Exception.Message
            });

            base.OnException(actionExecutedContext);
        }
    }
}