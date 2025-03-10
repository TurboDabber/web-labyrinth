import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Maze, Service, GenerateMazeCommand } from '../services/api/services.generated';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-maze-list',
  templateUrl: './mazelist.component.html',
  styles: [`
    .maze-controls {
      margin-bottom: 20px;
    }

    .input-group {
      margin-bottom: 10px;
      min-height: 78px;
    }
    .maze-list-container{
      margin-left: 20px;
      margin-top: 20px;
    }

    label {
      margin-right: 10px;
    }

    input {
      padding: 5px;
      border: 1px solid #ccc;
      border-radius: 4px;
    }

    .generate-button {
      padding: 10px 20px;
      background-color: #ff9800;
      color: white;
      border: none;
      border-radius: 4px;
      cursor: pointer;
    }

    .generate-button:disabled {
      background-color: #ccc;
      cursor: not-allowed;
    }

    .maze-list {
      display: flex;
      flex-direction: column;
      gap: 10px;
    }

    .maze-list.scrollable {
      max-height: 670px;
      overflow-y: auto;
    }

    .maze-button {
      padding: 10px;
      background-color: #ff9800;
      color: white;
      border: none;
      border-radius: 4px;
      cursor: pointer;
      text-align: center;
      margin-top: 10px;
    }

    .maze-button:hover {
      background-color: #e68900;
    }

    .maze-button.selected {
      background-color: #cc7a00;
      border: 2px solid #000;
    }

    .error-message {
      color: red;
      font-size: 12px;
    }
  `]
})
export class MazeListComponent implements OnInit {
  mazes: Maze[] = [];
  selectedMazeId: number | null = null;
  isGenerating: boolean = false;

  mazeForm!: FormGroup;

  @Output() mazeSelected = new EventEmitter<number>();

  constructor(private mazeService: Service) {}

  ngOnInit(): void {
    this.loadMazes();
    this.initForm();
  }

  loadMazes(): void {
    this.mazeService.mazesAll(undefined).subscribe((mazes) => {
      this.mazes = mazes.filter(maze => maze.id !== undefined);
    });
  }

  initForm(): void {
    this.mazeForm = new FormGroup({
      width: new FormControl(null, [
        Validators.required,
        Validators.min(1),
        this.oddNumberValidator
      ]),
      height: new FormControl(null, [
        Validators.required,
        Validators.min(1),
        this.oddNumberValidator
      ])
    });

  }

  oddNumberValidator(control: FormControl): { [key: string]: boolean } | null {
    const value = control.value;
    if (value && (value % 2 === 0 || value <= 0)) {
      return { 'oddNumber': true };
    }
    return null;
  }

  get widthControl() {
    return this.mazeForm.get('width')!;
  }

  get heightControl() {
    return this.mazeForm.get('height')!;
  }

  generateMaze(): void {
    if (this.mazeForm.valid) {
      const command = new GenerateMazeCommand();
      command.width = this.widthControl.value;
      command.height = this.heightControl.value;

      this.isGenerating = true;
      this.mazeService.mazesPOST(command).subscribe((mazeId) => {
        this.loadMazes();
        this.isGenerating = false;
      }, (error) => {
        console.error('Error generating maze:', error);
        this.isGenerating = false;
      });
    }
  }

  selectMaze(id: number): void {
    this.selectedMazeId = id;
    this.mazeSelected.emit(id);
  }
}
