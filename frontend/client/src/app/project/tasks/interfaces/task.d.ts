export interface ITask {
  taskId: number;
  title: string;
  description: string;
  dueDate: Date;
  isCompleted: boolean;
  assignedUserId: string;
}
