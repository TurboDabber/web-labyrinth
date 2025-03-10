import { Component, OnInit } from '@angular/core';
import { Maze, Service } from '../services/api/services.generated';

@Component({
  selector: 'app-main-view',
  template: `
    <div class="container">
      <div class="list">
        <app-maze-list (mazeSelected)="onMazeSelected($event)"></app-maze-list>
      </div>
      <div class="display">
        <app-maze [maze]="currentMaze"></app-maze>
      </div>
    </div>
  `,
  styles: [`
    .container {
      display: flex;
    }
    .list {
      width: 200px;
    }
    .display {
      flex-grow: 1;
    }
  `]
})
export class MainViewComponent implements OnInit {
  currentMaze: Maze | null = null;

  constructor(private mazeService: Service) {}

  ngOnInit(): void {}

  onMazeSelected(id: number): void {
    this.mazeService.mazesGET(id).subscribe((maze) => {
      this.currentMaze = maze;
    });
  }
}