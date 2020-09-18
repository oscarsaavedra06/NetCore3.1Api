using CoreBuenasPracticas.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace InfraestructureBuenasPracticas.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter //filtro para manejo de excepciones
    {

        //se valida si la excepcion que se activa en el filter es de tipo bussines exception para solo tener en cuenta este tipo de excepcion
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(BusinessException))
            {
                //se captura la excepcion que viene y se castea a tipo bussines exception
                var exception = (BusinessException)context.Exception;
                //objeto anónimo de respuesta, puede ser una clase 
                var validation = new
                {
                    status = 400,
                    Title = "Bad Request",
                    Detail = exception.Message
                };

                var json = new
                {
                    error = new[] { validation }
                };

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }
        }
    }
}
