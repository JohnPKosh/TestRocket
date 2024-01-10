using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.IO;

var builder = WebApplication.CreateBuilder (args);
var app = builder.Build ();

// A route to read a file
app.MapGet("/read", (HttpContext context) =>
{
    // Get the file name from the query string
    var fileName = context.Request.Query["file"];

    // Check if the file name is valid
    if (string.IsNullOrEmpty(fileName))
    {
        return Results.BadRequest("File name is required");
    }

    // Check if the file exists
    var filePath = Path.Combine(builder.Environment.ContentRootPath, fileName);
    if (!File.Exists(filePath))
    {
        return Results.NotFound("File not found");
    }

    // Read the file content
    var fileContent = File.ReadAllText(filePath);

    // Return the file content as a plain text response
    return Results.Text(fileContent);
});

// A route to write a file
app.MapPost("/write", (HttpContext context) =>
{
    // Get the file name from the query string
    var fileName = context.Request.Query["file"];

    // Check if the file name is valid
    if (string.IsNullOrEmpty(fileName))
    {
        return Results.BadRequest("File name is required");
    }

    // Get the file content from the request body
    var fileContent = context.Request.ReadFromJsonAsync<string>().Result;

    // Write the file content
    var filePath = Path.Combine(builder.Environment.ContentRootPath, fileName);
    File.WriteAllText(filePath, fileContent);

    // Return a success message as a plain text response
    return Results.Text("File written successfully");
});

app.Run ();
