import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { TaskListComponent } from '../task-list/task-list.component';
import { TaskFormComponent } from '../task-form/task-form.component';

@Component({
  selector: 'app-task-management',
  standalone: true,
  imports: [
    CommonModule,
    MatToolbarModule,
    MatButtonModule,
    TaskListComponent,
    TaskFormComponent,
  ],
  template: `
    <div class="content">
      <ng-container *ngIf="isTaskListView; else taskForm">
        <div class="actions">
          <button mat-raised-button color="primary" (click)="showTaskForm()">
            Create Task
          </button>
        </div>
        <app-task-list></app-task-list>
      </ng-container>
      <ng-template #taskForm>
        <app-task-form
          (taskCreated)="onTaskCreated()"
          (cancel)="showTaskList()"
        ></app-task-form>
      </ng-template>
    </div>
  `,
  styleUrls: ['./task-management.component.scss'],
})
export class TaskManagementComponent {
  isTaskListView = true;

  showTaskForm(): void {
    this.isTaskListView = false;
  }

  showTaskList(): void {
    this.isTaskListView = true;
  }

  onTaskCreated(): void {
    this.isTaskListView = true;
  }
}
