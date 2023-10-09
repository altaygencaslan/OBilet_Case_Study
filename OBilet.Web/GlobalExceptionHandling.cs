using System.Text.Json;

namespace OBilet.Web
{
    public class GlobalExceptionHandling
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<GlobalExceptionHandling> _logger;

        public GlobalExceptionHandling(RequestDelegate requestDelegate, ILogger<GlobalExceptionHandling> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate.Invoke(context);
            }
            catch (Exception ex)
            {
                string message = $"{DateTime.Now} - OBiletProject Error > Exception: {ex.ToString()}";
                _logger.LogError(message);

                context.Response.Redirect("Index");
            }
        }
    }
}
