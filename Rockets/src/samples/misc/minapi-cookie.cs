using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder (args);
var app = builder.Build ();

app.MapGet ("/getcookie", (HttpContext context) =>
{
    // Get the cookie value from the request header
    var cookie = context.Request.Cookies["mycookie"];

    // Return the cookie value as a plain text response
    return Results.Text(cookie ?? "No cookie found");
});

app.MapPost ("/setcookie", (HttpContext context) =>
{
    // Get the cookie value from the request body
    var cookieValue = context.Request.ReadFromJsonAsync<string>().Result;

    // Set the cookie options
    var cookieOptions = new CookieOptions()
    {
        Expires = DateTime.Now.AddMinutes(10),
        HttpOnly = true,
        Secure = true
    };

    // Append the cookie to the response header
    context.Response.Cookies.Append("mycookie", cookieValue, cookieOptions);

    // Return a success message as a plain text response
    return Results.Text("Cookie set successfully");
});

app.Run ();
