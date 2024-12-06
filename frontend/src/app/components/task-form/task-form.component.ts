import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { TaskService } from '../../services/task.service';
import { TaskInfo } from '../../models/task.models';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.scss'],
})
export class TaskFormComponent {
  taskInfo: TaskInfo = { name: '', description: '', assignedTo: '' };

  @Output() taskCreated = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>(); // Event for cancel action

  constructor(private taskService: TaskService) {}

  async createTask(): Promise<void> {
    try {
      await this.taskService.createTask(this.taskInfo);
      alert('Task created successfully!');
      this.taskCreated.emit(); // Notify parent component
    } catch (error) {
      console.error('Error creating task:', error);
      alert('Failed to create task. Please try again.');
    }
  }

  cancelForm(): void {
    this.cancel.emit(); // Emit cancel event to parent component
  }
}
