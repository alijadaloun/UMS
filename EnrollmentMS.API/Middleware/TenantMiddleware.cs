namespace EnrollmentMS.API.Middleware;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var tenantHeader = context.Request.Headers["X-Tenant"].ToString();
        
        if (string.IsNullOrWhiteSpace(tenantHeader))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Tenant header is required.");
            return;
        }

        context.Items["Tenant"] = tenantHeader;
        await _next(context);
    }
}