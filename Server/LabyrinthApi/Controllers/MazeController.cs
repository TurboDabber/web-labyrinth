using LabyrinthApi.Application.Commands;
using LabyrinthApi.Domain.Other;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/maze")]
public class MazeController : ControllerBase
{
    private readonly IMediator _mediator;

    public MazeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("maze/generate")]
    public async Task<IActionResult> GenerateMaze([FromBody] GenerateMazeCommand command)
    {
        if (command.Width <= 0 || command.Height <= 0)
        {
            return BadRequest("Width and height must be positive integers.");
        }

        var mazeId = await _mediator.Send(command);
        return Ok(new { MazeId = mazeId });
    }

    [HttpGet("maze/{id}")]
    public async Task<IActionResult> GetMaze(int id)
    {
        var command = new GetMazeCommand { Id = id };
        var maze = await _mediator.Send(command);

        if (maze == null)
        {
            return NotFound();
        }

        return Ok(maze);
    }

    [HttpGet("{id}/path")]
    public async Task<IActionResult> GetMazePath(
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