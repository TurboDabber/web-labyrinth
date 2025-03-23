using LabyrinthApi.Application.Queries.GetPathQuery;
using LabyrinthApi.Domain.Other;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LabyrinthApi.swagger
{
    public class NonNullablePoint2DSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(Point2D))
            {
                schema.Nullable = false;
            }

            if (context.Type == typeof(MazePathResponse))
            {
                if (schema.Properties.ContainsKey("path"))
                {
                    var pathSchema = schema.Properties["path"];
                    pathSchema.Nullable = false;
                    pathSchema.Items = new OpenApiSchema
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Point2D",
                            Type = ReferenceType.Schema
                        },
                        Nullable = false
                    };
                    if (pathSchema.Items != null)
                    {
                        pathSchema.Items.Nullable = false;
                    }
                }
            }
        }
    }
}
