using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DCT_106029.Controllers
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
            base.OnActionExecuting(actionContext);
        }
    }
}