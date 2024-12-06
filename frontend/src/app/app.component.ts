import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskManagementComponent } from './components/task-management/task-management.component';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, TaskManagementComponent, MatToolbarModule],
  template: `
    <mat-toolbar color="primary">
      <span>Task Management</span>
    </mat-toolbar>
    <div class="app-content">
      <app-task-management></app-task-management>
    </div>
  `,
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {}
