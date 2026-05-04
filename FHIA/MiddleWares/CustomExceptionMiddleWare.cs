using BLL.Dtos.errors;
using DAL.Exceptions;

namespace FHIA.MiddleWares
{
    public class CustomExceptionMiddleWare(RequestDelegate _next , ILogger<CustomExceptionMiddleWare> _logger)
    {

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                if(context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.Response.ContentType = "application/json";
                    var errorResponse = new CustomError
                    {
                        Message = $"Resource with this path {context.Request.Path} : not found",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                    await context.Response.WriteAsJsonAsync(errorResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("error happen : {ex}",ex);
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var errorResponse = new CustomError
                {
                    Message = ex.Message,
                    StatusCode = ex switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError,
                    }
                };
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
