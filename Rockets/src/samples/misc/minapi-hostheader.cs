using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder (args);
var app = builder.Build ();

app.MapGet ("/host", (HttpContext context) =>
{
    // Get the host header from the request
    var host = context.Request.Host;

    // Get the domain name from the host header
    var domain = host.Host;

    // Return the host and domain as a JSON object
    return Results.Json (new { host, domain });
});

app.Run ();
