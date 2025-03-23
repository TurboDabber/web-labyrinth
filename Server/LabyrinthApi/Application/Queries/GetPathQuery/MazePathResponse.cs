using LabyrinthApi.Domain.Other;

namespace LabyrinthApi.Application.Queries.GetPathQuery
{
    public record MazePathResponse(IEnumerable<Point2D> Path)
    {
        public IEnumerable<Point2D> Path { get; } = Path ?? Array.Empty<Point2D>();
    }
}
