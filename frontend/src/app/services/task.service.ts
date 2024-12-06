import axios, { AxiosInstance } from 'axios';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { TaskInfo, TaskView, ChangeTaskStatusInfo } from '../models/task.models';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  private axiosClient: AxiosInstance;

  constructor() {
    this.axiosClient = axios.create({
      baseURL: `${environment.backendBaseUrl}/Tasks`, // Use the backend URL from the environment
      headers: {
        'Content-Type': 'application/json',
      },
    });
  }

  async createTask(taskInfo: TaskInfo): Promise<TaskView> {
    const response = await this.axiosClient.post<TaskView>('', taskInfo);
    return response.data;
  }

  async updateTaskStatus(id: number, statusInfo: ChangeTaskStatusInfo): Promise<void> {
    await this.axiosClient.put(`${id}/status`, statusInfo);
  }

  async getTasks(skip: number, take: number): Promise<TaskView[]> {
    const response = await this.axiosClient.get<TaskView[]>('', {
      params: { skip, take },
    });
    return response.data;
  }
}
