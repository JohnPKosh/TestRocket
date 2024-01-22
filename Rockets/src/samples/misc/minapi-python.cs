using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.IO;

var builder = WebApplication.CreateBuilder (args);
var app = builder.Build ();

// A route to execute a Python script
app.MapPost("/execute", (HttpContext context) =>
{
    // Get the script name from the query string
    var scriptName = context.Request.Query["script"];

    // Check if the script name is valid
    if (string.IsNullOrEmpty(scriptName))
    {
        return Results.BadRequest("Script name is required");
    }

    // Get the script arguments from the request body
    var scriptArgs = context.Request.ReadFromJsonAsync<string[]>().Result;

    // Create a process start info with the python executable and the script name and arguments
    var start = new ProcessStartInfo();
    start.FileName = "python.exe";
    start.Arguments = string.Format("{0} {1}", scriptName, string.Join(" ", scriptArgs));
    start.UseShellExecute = false;
    start.RedirectStandardOutput = true;
    start.RedirectStandardError = true;

    // Start the process and get the output and error
    using (var process = Process.Start(start))
    {
        using (var reader = process.StandardOutput)
        {
            var output = reader.ReadToEnd();
            Console.WriteLine(output);
        }
        using (var reader = process.StandardError)
        {
            var error = reader.ReadToEnd();
            Console.WriteLine(error);
        }
    }

    // Return a success message as a plain text response
    return Results.Text("Script executed successfully");
});

app.Run ();
