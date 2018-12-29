﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MP.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(IsApiRoute(context.HttpContext))
            {
                var apiError = ApiError.Create("Internal server error.", context.Exception.Message);
                context.Result = new ObjectResult(apiError)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        private static bool IsApiRoute(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/api");
        }
    }
}
