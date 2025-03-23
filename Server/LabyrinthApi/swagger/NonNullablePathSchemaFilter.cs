using LabyrinthApi.Application.Queries.GetPathQuery;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class NonNullablePathSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(MazePathResponse))
        {
            if (schema.Properties.ContainsKey("path"))
            {
                schema.Properties["path"].Nullable = false;
            }
        }
    }
}