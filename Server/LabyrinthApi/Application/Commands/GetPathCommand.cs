﻿using MediatR;
using LabyrinthApi.Domain.Other;

namespace LabyrinthApi.Application.Commands;

public class GetPathCommand : IRequest<Point2D[]>
{
    public int Id { get; set; }
    public required Point2D Start { get; set; }
    public required Point2D End { get; set; }
}
