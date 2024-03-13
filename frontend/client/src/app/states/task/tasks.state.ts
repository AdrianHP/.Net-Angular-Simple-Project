import { Task } from "src/app/project/tasks/interfaces/task";


export interface TasksState {
  tasks: Task[];
}

export const initialState: TasksState = {
  tasks: [],
};
