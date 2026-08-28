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
            _logger.LogError(exception, "Beklenmeyen bir hata oluştu: {Message}", exception.Message);

            var (statusCode, title, detail) = MapException(exception);

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private static (int StatusCode, string Title, string Detail) MapException(Exception exception) => exception switch
        {
            FluentValidation.ValidationException validationEx => (
                StatusCodes.Status400BadRequest,
                "Doğrulama Hatası",
                string.Join(" | ", validationEx.Errors.Select(e => e.ErrorMessage))
            ),

            UnauthorizedAccessException => (
                StatusCodes.Status401Unauthorized,
                "Yetkisiz Erişim",
                "Bu işlem için yetkiniz bulunmamaktadır."
            ),

            KeyNotFoundException => (
                StatusCodes.Status404NotFound,
                "Kayıt Bulunamadı",
                exception.Message
            ),

            InvalidOperationException => (
                StatusCodes.Status400BadRequest,
                "Geçersiz İşlem",
                exception.Message
            ),

            _ => (
                StatusCodes.Status500InternalServerError,
                "Sunucu Hatası",
                "Beklenmeyen bir hata oluştu. Lütfen daha sonra tekrar deneyin."
            )
        };
    }
}