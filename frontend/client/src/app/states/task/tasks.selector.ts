import { createFeatureSelector, createSelector } from "@ngrx/store";
import { TasksState } from "./tasks.state";

export const selectTasks = createFeatureSelector<TasksState>("tasks");

export const selectTaskItems = createSelector(
  selectTasks,
  (state: TasksState) => state.tasks
);

export const selectTaskItem = (props: { id: number }) =>
  createSelector(selectTaskItems, (taskItems) =>
    taskItems.find((taskItem) => taskItem.taskId === props.id)
  );
