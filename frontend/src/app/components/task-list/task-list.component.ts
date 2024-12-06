import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginatorModule } from '@angular/material/paginator';
import { TaskService } from '../../services/task.service';
import { TaskView } from '../../models/task.models';

@Component({
  selector: 'app-task-list',
  standalone: true, // Declare this as a standalone component
  imports: [CommonModule, MatTableModule, MatButtonModule, MatPaginatorModule], // Import required modules
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss'],
})
export class TaskListComponent implements OnInit {
  tasks: TaskView[] = [];
  displayedColumns: string[] = ['id', 'name', 'status', 'assignedTo', 'actions'];
  skip = 0;
  take = 10;

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.fetchTasks();
  }

  async fetchTasks(): Promise<void> {
    this.tasks = await this.taskService.getTasks(this.skip, this.take);
  }

  async changeStatus(taskId: number, newStatus: number): Promise<void> {
    try {
      await this.taskService.updateTaskStatus(taskId, { newStatus });
      this.fetchTasks(); // Refresh task list after updating status
    } catch (error) {
      console.error('Error updating task status:', error);
    }
  }

  onPageChange(event: any): void {
    this.skip = event.pageIndex * event.pageSize;
    this.take = event.pageSize;
    this.fetchTasks();
  }
}
