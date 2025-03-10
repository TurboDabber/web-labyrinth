import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MazeComponent } from './maze/maze.component';
import { MazeListComponent } from './mazelist/mazelist.component';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { MainViewComponent } from './main-view/main-view.component';
import { API_BASE_URL } from './services/api/services.generated';
import { ReactiveFormsModule } from '@angular/forms';  

@NgModule({
  declarations: [
    AppComponent,
    MazeComponent,
    MazeListComponent,
    MainViewComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CommonModule,
    ReactiveFormsModule
  ],
  providers: [
    { provide: API_BASE_URL, useValue: 'https://localhost:7039' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
