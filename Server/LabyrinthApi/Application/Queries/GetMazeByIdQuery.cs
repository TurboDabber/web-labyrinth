using LabyrinthApi.Application.Commands;
using LabyrinthApi.Domain.Entities;
using LabyrinthApi.Domain.Interfaces;
using MediatR;

namespace LabyrinthApi.Application.Queries;

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

