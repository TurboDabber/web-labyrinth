using LabyrinthApi.Domain.Interfaces;
using LabyrinthApi.Domain.Other;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabyrinthApi.Application.Services;

public class DijkstraPathFinder: IPathFinder
{
    public List<Point2D> FindPath(int[,] maze, Point2D start, Point2D end)
    {
        if (start.Equals(end))
            return new List<Point2D> { start };

        int rows = maze.GetLength(0);
        int cols = maze.GetLength(1);

        // Directions for moving in the maze (up, down, left, right)
        var directions = new[]
        {
            new Point2D(0, 1),   // Right
            new Point2D(1, 0),   // Down
            new Point2D(0, -1),  // Left
            new Point2D(-1, 0)   // Up
        };

        var priorityQueue = new PriorityQueue<(int distance, Point2D point), int>();
        var distanceMap = new Dictionary<Point2D, int>();
        var previousMap = new Dictionary<Point2D, Point2D>();

        // Initialize distances to infinity for all points and start with distance 0 for the start point.
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                distanceMap[new Point2D(i, j)] = int.MaxValue;
            }
        }
        distanceMap[start] = 0;

        // Push the starting point into the priority queue.
        priorityQueue.Enqueue((0, start), 0);

        while (priorityQueue.Count > 0)
        {
            var (currentDistance, currentPoint) = priorityQueue.Dequeue();

            // If we have reached the end, reconstruct and return the path.
            if (currentPoint.Equals(end))
            {
                return ReconstructPath(previousMap, start, end);
            }

            // Explore the neighboring cells (up, down, left, right).
            foreach (var direction in directions)
            {
                var neighbor = new Point2D(currentPoint.x + direction.x, currentPoint.y + direction.y);

                // Check if the neighbor is within bounds and not a wall.
                if (IsValid(neighbor, maze, rows, cols))
                {
                    int newDistance = currentDistance + 1; // Each move has a cost of 1.
                    if (newDistance < distanceMap[neighbor])
                    {
                        distanceMap[neighbor] = newDistance;
                        previousMap[neighbor] = currentPoint;
                        priorityQueue.Enqueue((newDistance, neighbor), newDistance);
                    }
                }
            }
        }

        // If no path is found, return an empty list.
        return new List<Point2D>();
    }

    private bool IsValid(Point2D point, int[,] maze, int rows, int cols)
    {
        // Check if the point is within the bounds of the maze and not a wall.
        return point.x >= 0 && point.x < rows && point.y >= 0 && point.y < cols && maze[point.x, point.y] == 0;
    }

    private List<Point2D> ReconstructPath(Dictionary<Point2D, Point2D> previousMap, Point2D start, Point2D end)
    {
        var path = new List<Point2D>();
        var currentPoint = end;

        // Reconstruct the path from end to start by following the previous points.
        while (currentPoint != null && !currentPoint.Equals(start))
        {
            path.Add(currentPoint);
            currentPoint = previousMap.ContainsKey(currentPoint) ? previousMap[currentPoint] : null;
        }

        if (currentPoint != null)
            path.Add(start);

        // Reverse the path to get it from start to end.
        path.Reverse();
        return path;
    }
}
