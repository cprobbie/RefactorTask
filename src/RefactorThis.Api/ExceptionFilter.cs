using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RefactorThis.Api
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var errorMessage = "Internal Server Error";
            var code = HttpStatusCode.InternalServerError;
            if (context.Exception is ArgumentException argEx)
            {
                code = HttpStatusCode.BadRequest;
                errorMessage = argEx.Message;
            }
            else if (context.Exception is KeyNotFoundException notFoundEx)
            {
                code = HttpStatusCode.NotFound;
                errorMessage = notFoundEx.Message;
            }
            else if (context.Exception is Exception ex)
            {
                errorMessage = errorMessage + ": " + ex.Message;
            }

            var result = new ObjectResult(errorMessage) { StatusCode = (int)code};
            context.Result = result;
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
        }
    }
}
