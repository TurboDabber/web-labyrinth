﻿using LabyrinthApi.Application.Commands;
using LabyrinthApi.Domain.Entities;
using LabyrinthApi.Domain.Interfaces;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace LabyrinthApi.Application.Queries;

public class GenerateMazeQuery : IRequestHandler<GenerateMazeCommand, Maze>
{
    private readonly IMazeService _mazeService;

    public GenerateMazeQuery(IMazeService mazeService)
    {
        _mazeService = mazeService;
    }

    public async Task<Maze> Handle(GenerateMazeCommand request, CancellationToken cancellationToken)
    {
        var mazeId = await _mazeService.GenerateMazeAsync(request.Width, request.Height);
        var maze = await _mazeService.GetMazeAsync(mazeId);
        if (maze == null)
        {
            throw new ArgumentNullException(nameof(maze), "The new Maze could be not loaded");
        }
        return maze;            
    }
}
