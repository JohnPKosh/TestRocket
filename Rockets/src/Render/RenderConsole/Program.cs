using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RenderConsole;
using RazorClassLibrary;

IServiceCollection services = new ServiceCollection();
services.AddLogging();

IServiceProvider serviceProvider = services.BuildServiceProvider();
ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

await using var htmlRenderer = new HtmlRenderer(serviceProvider, loggerFactory);

var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
{
  var dictionary = new Dictionary<string, object?>
    {
        { "Message", "Walla Walla & Washington" }
    };

  var parameters = ParameterView.FromDictionary(dictionary);
  //var output = await htmlRenderer.RenderComponentAsync<RenderMessage>(parameters);
  var output = await htmlRenderer.RenderComponentAsync<LibraryMessage>(parameters);

  return output.ToHtmlString();
});

Console.WriteLine(html);