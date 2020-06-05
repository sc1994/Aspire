using System;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Aspire.Application.AppServices
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorAppService : AppService
    {
        //[HttpGet, HttpPost, HttpPut, HttpDelete]
        //[Route("/error")]
        //public dynamic Error()
        //{
        //    var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

        //    switch (context.Error)
        //    {

        //        case NotImplementedException notImplemented:
        //            return new ErrorOutputDto("此API还未实现, 请联系系统管理员");
        //        default:
        //            var l = context.Error.Message.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        //            return new ErrorOutputDto(context.Error.GetType().FullName, l);
        //    }
        //}

        //private dynamic ResponseError(string title, Exception ex, string[] details = null)
        //{

        //}
    }
}
