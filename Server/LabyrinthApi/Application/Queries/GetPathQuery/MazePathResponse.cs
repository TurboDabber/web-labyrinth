using LabyrinthApi.Domain.Other;

namespace LabyrinthApi.Application.Queries.GetPathQuery
{
    public record MazePathResponse(IEnumerable<Point2D> Path);
}
