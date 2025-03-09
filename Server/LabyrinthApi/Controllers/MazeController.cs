using MediatR;
using Microsoft.AspNetCore.Mvc;
using LabyrinthApi.Application.Commands;
using LabyrinthApi.Application.Queries.GetPathQuery;
using LabyrinthApi.Domain.Entities;
using LabyrinthApi.Domain.Other;

[ApiController]
[Route("api/maze")]
public class MazeController : ControllerBase
{
    private readonly IMediator _mediator;

    public MazeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("mazes")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<ActionResult<int>> GenerateMaze([FromBody] GenerateMazeCommand command)
    {
        if (command.Width <= 0 || command.Height <= 0)
        {
            return BadRequest("Width and height must be positive integers.");
        }

        var mazeId = await _mediator.Send(command);
        return Ok(new { MazeId = mazeId });
    }

    [HttpGet("mazes")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Maze[]))]
    public async Task<ActionResult<Maze[]>> GetAllMazes([FromQuery]GetAllMazes command)
    {
        var mazes = await _mediator.Send(command);
        return Ok(mazes);
    }

    [HttpGet("mazes/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Maze))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Maze>> GetMaze(int id)
    {
        var command = new GetMazeCommand { Id = id };
        var maze = await _mediator.Send(command);

        if (maze == null)
        {
            return NotFound();
        }

        return Ok(maze);
    }

    [HttpGet("mazes/{id}/path")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MazePathResponse))]
    public async Task<ActionResult<MazePathResponse>> GetMazePath(
               int id,
               [FromQuery] int startX,
               [FromQuery] int startY,
               [FromQuery] int endX,
               [FromQuery] int endY)
    {
        var start = new Point2D(startX, startY);
        var end = new Point2D(endX, endY);

        GetPathCommand command = new GetPathCommand
        {
            Id = id,
            Start = start,
            End = end
        };

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return Ok(new
            {
                Path = Array.Empty<Point2D>()
            });
        }

        return Ok(new
        {
            Path = result
        });
    }

}