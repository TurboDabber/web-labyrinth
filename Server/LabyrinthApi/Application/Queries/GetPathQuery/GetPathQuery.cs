using MediatR;
using LabyrinthApi.Application.Commands;
using LabyrinthApi.Domain.Interfaces;
using LabyrinthApi.Domain.Other;

namespace LabyrinthApi.Application.Queries.GetPathQuery;

public class GetPathQuery : IRequestHandler<GetPathCommand, Point2D[]>
{
    private readonly IMazeService _mazeService;

    public GetPathQuery(IMazeService mazeService)
    {
        _mazeService = mazeService;
    }


    async public Task<Point2D[]> Handle(GetPathCommand request, CancellationToken cancellationToken)
    {
        var mazeData = await _mazeService.SolveMazeAsync(request.Id, request.Start, request.End);

        if (mazeData == null)
        {
            return Array.Empty<Point2D>();
        }
        return mazeData.ToArray();
    }
}
