using System;
using System.Net;
using System.Text.Json;
using System.Globalization;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc; 
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WebApi5.Middlewares
{
    public class ExcepcionesMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHttpContextAccessor accessor;
        private readonly ILogger<ExcepcionesMiddleware> logger;

        public ExcepcionesMiddleware(RequestDelegate next,IHttpContextAccessor accessor, ILogger<ExcepcionesMiddleware> logger){
            this.next=next;
            this.accessor=accessor;
            this.logger=logger;
        }

        public async Task Invoke(HttpContext context){
            try{
                await next.Invoke(context);
            }
            catch(Exception ex){
                logger.LogError(ex,$"Ocurrio un error durante el proceso {accessor.HttpContext.TraceIdentifier}.");
                await ManejarMensajeAsync(accessor.HttpContext).ConfigureAwait(false);
            }
        }

        private static Task ManejarMensajeAsync(HttpContext context){
            string response=JsonSerializer.Serialize(new ValidationProblemDetails(){
                Title="Un error ocurrio.",
                Status=(int)HttpStatusCode.InternalServerError,
                Instance=context.Request.Path,
            });
            context.Response.ContentType="application/json";
            context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(response);
        }

    }
}