using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SchoolSystem.WebAPI.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) => _logger = logger;
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Beklenmeyen bir hata oluştu.");

            var (statusCode, title, detail) = MapException(exception);

            var problemDetails = new ProblemDetails { Status = statusCode, Title = title, Detail = detail };
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private static (int, string, string) MapException(Exception exception) => exception switch
        {
            FluentValidation.ValidationException validationEx => (400, "Doğrulama hatası", string.Join(" | ", validationEx.Errors.Select(e => e.ErrorMessage))),
            InvalidOperationException => (409, "İşlem gerçekleştirilemedi.", exception.Message),
            UnauthorizedAccessException => (401, "Yetkisiz erişim", exception.Message),
            _ => (500, "Beklenmeyen bir hata oluştu.", "Lütfen daha sonra tekrar deneyin.")

        };
    }
}
