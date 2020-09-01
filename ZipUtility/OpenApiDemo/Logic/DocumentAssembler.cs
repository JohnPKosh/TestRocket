using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace OpenApiDemo.Logic
{
  public class DocumentAssembler
  {
    public void CreateMockDoc()
    {
      var document = new OpenApiDocument
      {
        Info = new OpenApiInfo
        {
          Version = "1.0.0",
          Title = "Swagger Petstore (Simple)",
          Description = "API description goes here",
          TermsOfService = new Uri("http://petstore.swagger.io/termsofservice"),
          Contact = new OpenApiContact()
          {
            Name = "The API Team",
            Url = new Uri("http://petstore.swagger.io"),
            Email = "apiteam@swagger.io"
          }
        },
        Servers = new List<OpenApiServer>
        {
            new OpenApiServer {
              Url = "http://petstore.swagger.io/api",
              Description = "API production server description goes here!"
            },
            new OpenApiServer {
              Url = "http://testpet.swagger.io/api",
              Description = "API test server description goes here!"
            }
        },
        Paths = new OpenApiPaths
        {
          ["/pets"] = new OpenApiPathItem
          {
            Operations = new Dictionary<OperationType, OpenApiOperation>
            {
              [OperationType.Get] = new OpenApiOperation
              {
                Description = "Returns all pets from the system that the user has access to",
                Responses = new OpenApiResponses
                {
                  ["200"] = new OpenApiResponse
                  {
                    Description = "OK"
                  }
                }
              },
            }
          }
        }
      };
    }
  }
}

/*
https://swagger.io/docs/specification/basic-structure/
https://github.com/microsoft/OpenAPI.NET

*/
