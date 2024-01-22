To call a C# minimal API from alexa, you need to create an alexa skill that can invoke the API endpoint using an HTTP request. You can use the Alexa.NET library to help you with the alexa skill development in C#. Here are the basic steps to follow:

- Create a new C# project using the Alexa.NET template. This will create a lambda function that handles the alexa skill requests and responses.
- Install the Alexa.NET.RequestHandlers package to handle different types of intents and requests.
- Define your intents and utterances in the Alexa Developer Console. For example, you can create an intent called `CallApiIntent` with an utterance like `call the API with {command}`.
- Add a slot called `command` to the `CallApiIntent` and specify its type and values. For example, you can use a custom slot type called `COMMAND` and add some sample values like `turn on the light` or `play some music`.
- Add a request handler for the `CallApiIntent` in your C# code. You can use the `IntentRequestHandler` class and override the `Handle` method. In this method, you can get the slot value from the request and use the `HttpClient` class to send an HTTP request to the minimal API endpoint with the slot value as the command. You can also use the `ResponseBuilder` class to create a response for the alexa skill with a speech or card output.
- Deploy your C# project to AWS Lambda and link it to your alexa skill in the Alexa Developer Console.
- Test your alexa skill using the simulator or a real device. You should be able to invoke the minimal API by saying something like `Alexa, ask my skill to call the API with turn on the light`.

For more information, you can check out these resources:

- [Alexa Skill With .NET Core](^3^)
- [Alexa.NET: An Amazon Alexa Skills SDK for .NET](^1^)
- [Alexa.NET.RequestHandlers: A library to simplify request handling in Alexa.NET](^2^)
- [Mastering Minimal APIs in .NET 8 with Examples](^4^)

Source: Conversation with Bing, 1/7/2024
(1) Alexa Skill With .NET Core - DZone. https://dzone.com/articles/alexa-skill-withnet-core.
(2) timheuer/alexa-skills-dotnet: An Amazon Alexa Skills SDK for .NET - GitHub. https://github.com/timheuer/alexa-skills-dotnet.
(3) Mastering Minimal APIs in .NET 8 with Examples | CodeNx - Medium. https://medium.com/codenx/minimal-apis-in-net-8-a-simplified-approach-to-build-services-eb50df56819f.
(4) Get started with minimal API for .NET 6 - DEV Community. https://dev.to/dotnet/get-started-with-minimal-api-1il7.