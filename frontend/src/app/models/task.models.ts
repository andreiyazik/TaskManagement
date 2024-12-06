export interface TaskInfo {
    name: string;
    description: string;
    assignedTo?: string;
  }
  
  export interface ChangeTaskStatusInfo {
    newStatus: number;
  }
  
  export interface TaskView {
    id: number;
    name: string;
    description: string;
    status: string;
    assignedTo?: string;
  }
  