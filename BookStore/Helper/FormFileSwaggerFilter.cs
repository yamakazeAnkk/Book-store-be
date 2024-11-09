using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BookStore.Helper
{
    public class FormFileSwaggerFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Get all parameters of type IFormFile or IEnumerable<IFormFile>
            var fileParams = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile) || p.ParameterType == typeof(IEnumerable<IFormFile>))
                .ToList();

            // If no file parameters are present, exit early
            if (!fileParams.Any()) return;

            // Ensure multipart/form-data is defined
            if (operation.RequestBody == null || !operation.RequestBody.Content.ContainsKey("multipart/form-data"))
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content = new Dictionary<string, OpenApiMediaType>()
                };
            }

            // Initialize schema for multipart/form-data
            var schema = new OpenApiSchema
            {
                Type = "object",
                Properties = new Dictionary<string, OpenApiSchema>()
            };

            // Add all IFormFile parameters to the schema
            foreach (var param in fileParams)
            {
                schema.Properties.Add(param.Name, new OpenApiSchema
                {
                    Type = "string",
                    Format = "binary"
                });
            }

            // Check if the method has an additional "BookJson" or "UserJson" parameter
      

            var userJsonParam = context.MethodInfo.GetParameters()
                .FirstOrDefault(p => p.Name.Equals("userJson", StringComparison.OrdinalIgnoreCase) && p.ParameterType == typeof(string));

            
            if (userJsonParam != null)
            {
                // Add the UserDto schema properties under the key "UserJson"
                schema.Properties.Add("UserJson", new OpenApiSchema
                {
                    Type = "object",
                    Properties = new Dictionary<string, OpenApiSchema>
                    {

                        ["username"] = new OpenApiSchema { Type = "string" },
                        ["password"] = new OpenApiSchema { Type = "string" },
                        ["email"] = new OpenApiSchema { Type = "string", Format = "email" },
                        ["phone"] = new OpenApiSchema { Type = "string", Nullable = true },
                        ["fullname"] = new OpenApiSchema { Type = "string", Nullable = true },
                        ["address"] = new OpenApiSchema { Type = "string", Nullable = true }
                    },
                    // Define required fields within the "UserDto" object
                    Required = new HashSet<string> { "username", "email", "password" }
                });

                // Include "imageFile" and "UserJson" as required fields
                schema.Required = new HashSet<string> { "imageFile", "UserJson" };
            }
            else
            {
                // When only imageFile is present, mark it as required
                schema.Required = new HashSet<string>(fileParams.Select(p => p.Name));
            }

            // Apply the schema to the operation's request body for multipart/form-data
            operation.RequestBody.Content["multipart/form-data"] = new OpenApiMediaType
            {
                Schema = schema
            };
        }
    }
}
