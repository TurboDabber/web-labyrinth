using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using LabyrinthApi.Domain.Enums;

namespace LabyrinthApi.Domain.Entities;

public class Maze
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int Width { get; set; }

    [Required]
    public int Height { get; set; }

    [Required]
    public string? MazeDataJson { get; set; }

    [NotMapped]
    public int[][] MazeData
    {
        get => MazeDataJson != null ? (JsonConvert.DeserializeObject<int[][]>(MazeDataJson) ?? new int[][] {}) : new int[][] { };
        set => MazeDataJson = JsonConvert.SerializeObject(value);
    }

    [Required]
    public MazeAlgorithmType AlgorithmType { get; set; }
    public Maze() { }
    public Maze(int id, int width, int height, int [][] mazeData, MazeAlgorithmType type = MazeAlgorithmType.RecursiveBacktracking)
    {
        Id = id;
        Width = width;
        Height = height;
        MazeData = mazeData;
        AlgorithmType = type;
    }
}