using LabyrinthApi.Domain.Entities;
using MediatR;

namespace LabyrinthApi.Application.Commands;

public class GetMazeCommand : IRequest<Maze?>
{
    public int Id { get; set; }
}
