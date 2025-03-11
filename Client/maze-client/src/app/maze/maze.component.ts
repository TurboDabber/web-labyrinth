import { Component, Input, SimpleChanges } from '@angular/core';
import { Maze, Service, MazePathResponse, Point2D } from '../services/api/services.generated';

@Component({
  selector: 'app-maze',
  templateUrl: './maze.component.html',
  styles: [`
    .row {
      display: flex;
    }
    
    .button-reset{
      margin-top: 25px;
      max-width: 90px;
      max-height: 45px;
      margin-left: 40px;
    }

    .wall {
      width: 20px;
      height: 20px;
      background-color: grey;
    }
    .ground {
      width: 20px;
      height: 20px;
      background-color: red;
    }
    .ground:hover {
      background-color: darkred; 
    }
    .selected {
      background-color: orange; 
    }
    .path {
      background-color: blue; 
    }
    .maze-container {
      display: flex;
      flex-direction: row;
      margin-left: 50px;
      margin-top: 50px;
    }
  `]
})
export class MazeComponent {
  @Input() maze: Maze | null = null;
  selectedCells: Point2D[] = []; 
  pathCells: Point2D[] = []; 

  constructor(private mazeService: Service) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['maze'] && this.maze) {
      this.resetSelection();
    }
  }

  onCellClick(x: number, y: number): void {
    if (this.maze?.mazeData?.[x]?.[y] === 0) { 
      const cell = new Point2D({ x: x, y: y });

      const index = this.selectedCells.findIndex(c => c.x === x && c.y === y);
      if (index !== -1) {
        this.selectedCells.splice(index, 1);
        this.pathCells = [];
      } else {
        if (this.selectedCells.length < 2) {
          this.selectedCells.push(cell);
        }
      }

      if (this.selectedCells.length === 2) {
        this.calculatePath();
      }
    }
  }

  resetSelection(): void {
    this.selectedCells = [];
    this.pathCells = [];
  }

  calculatePath(): void {
    if (this.maze?.id && this.selectedCells.length === 2) {
      const [start, end] = this.selectedCells;
      this.mazeService.path(
        this.maze.id,
        start.x,
        start.y,
        end.x,
        end.y
      ).subscribe((response: MazePathResponse) => {
        if (response.path) {
          this.pathCells = response.path.map(p => new Point2D({ x: p.x, y: p.y }));
        }
      });
    }
  }

  isSelected(x: number, y: number): boolean {
    return this.selectedCells.some(c => c.x === x && c.y === y);
  }

  isPath(x: number, y: number): boolean {
    return this.pathCells.some(c => c.x === x && c.y === y);
  }
}