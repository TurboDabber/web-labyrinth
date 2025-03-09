using MediatR;
using LabyrinthApi.Application.Commands;
using LabyrinthApi.Domain.Entities;
using LabyrinthApi.Domain.Interfaces;

namespace LabyrinthApi.Application.Queries.GetAllMazesQuery;

public class GetAllMazesQuery : IRequestHandler<GetAllMazes, Maze[]>
{

    private readonly IMazeService _mazeService;

    public GetAllMazesQuery(IMazeService mazeService)
    {
        _mazeService = mazeService;
    }

    public async Task<Maze[]> Handle(GetAllMazes request, CancellationToken cancellationToken)
    {
        var maze = await _mazeService.GetAllMazes();

        return maze;
    }
}
