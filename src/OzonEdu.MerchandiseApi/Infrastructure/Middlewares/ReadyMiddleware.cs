﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
#pragma warning disable 1591

namespace OzonEdu.MerchandiseApi.Infrastructure.Middlewares
{
    public class ReadyMiddleware
    {
        public ReadyMiddleware(RequestDelegate next){}

        public async Task InvokeAsync(HttpContext context) 
            => await context.Response.WriteAsync("200 Ok");
    }
}