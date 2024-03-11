import { Task } from "src/app/models/task";


export interface TasksState {
  tasks: Task[];
}

export const initialState: TasksState = {
  tasks: [],
};
