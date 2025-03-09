using MediatR;
using LabyrinthApi.Application.Commands;
using LabyrinthApi.Domain.Entities;
using LabyrinthApi.Domain.Interfaces;

namespace LabyrinthApi.Application.Queries.GetMazeByIdQuery;

public class GetMazeByIdQuery : IRequestHandler<GetMazeCommand, Maze?>
{

    private readonly IMazeService _mazeService;

    public GetMazeByIdQuery(IMazeService mazeService)
    {
        _mazeService = mazeService;
    }

    public async Task<Maze?> Handle(GetMazeCommand request, CancellationToken cancellationToken)
    {
        var maze = await _mazeService.GetMazeAsync(request.Id);

        return maze;
    }
}

