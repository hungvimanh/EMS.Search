using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EMS.Search
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            if (exception is NotFoundException || exception is UnauthorizedException)
            {
                if (exception is NotFoundException) code = HttpStatusCode.NotFound;
                else if (exception is UnauthorizedException) code = HttpStatusCode.Unauthorized;

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
                return context.Response.WriteAsync(JsonConvert.SerializeObject(exception));
            }
            else if (exception is MessageException)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 420;
                return context.Response.WriteAsync(exception.Message);

            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(exception.Message);

        }
    }


    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string Message) : base(Message)
        {
        }
    }

    public class MessageException : Exception
    {
        private static string ModifyMessage(object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
        public MessageException(string message) : base(message) { }

        public MessageException(Exception ex) : base(ModifyMessage(ex)) { }

        public MessageException(ModelStateDictionary ms) : base(ModifyMessage(ms)) { }
    }

    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string Message) : base(Message) { }
    }

    public class ForbiddenException : Exception
    {
        public ForbiddenException(string Message) : base(Message)
        {
        }
    }

    public class NotFoundException : Exception
    {
        //private static string ModifyMessage()
        //{
        //    return JsonConvert.SerializeObject(message);
        //}
        public NotFoundException(string Message) : base(Message)
        {
        }
    }

    public class ConflictException : Exception
    {
        public ConflictException(string Message) : base(Message)
        {
        }
    }
    public class BadRequestException : Exception
    {
        private static string ModifyMessage(object DataDTO)
        {
            return JsonConvert.SerializeObject(DataDTO, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
        public BadRequestException(string Message) : base(Message)
        {
        }
        public BadRequestException(object obj) : base(ModifyMessage(obj))
        {
        }
    }
}