using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

public class FixMissingSwaggerProperties : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties == null)
            return;

        foreach (var property in schema.Properties)
        {
            if (string.IsNullOrEmpty(property.Value.Type))
            {
                property.Value.Type = "string";
            }
        }
    }
}
