namespace LabyrinthApi.Domain.Interfaces;

public interface IMazeGenerator
{
    int[,] GenerateMaze(int width, int height);
}
