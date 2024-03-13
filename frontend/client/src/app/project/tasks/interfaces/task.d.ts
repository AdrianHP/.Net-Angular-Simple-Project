export interface ITask {
  id: number;
  title: string;
  description: string;
  dueDate: Date;
  isCompleted: boolean;
  assignedUserId: string;
}
