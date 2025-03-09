using MediatR;
using LabyrinthApi.Domain.Entities;

namespace LabyrinthApi.Application.Commands;

public class GenerateMazeCommand : IRequest<Maze>
{
    public int Width { get; set; }
    public int Height { get; set; }
}
