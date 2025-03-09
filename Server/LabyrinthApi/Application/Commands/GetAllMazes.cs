using MediatR;
using LabyrinthApi.Domain.Entities;

namespace LabyrinthApi.Application.Commands;

public class GetAllMazes() : IRequest<Maze[]>;

