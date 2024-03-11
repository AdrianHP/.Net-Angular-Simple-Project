import { createAction, props } from "@ngrx/store";
import { Task } from "src/app/models/task";

export const appLoaded = createAction("[App] App Loaded");

export const fetchTaskSuccess = createAction(
  "[Task API] Fetch Task Success",
  props<{ tasks: Task[] }>()
);

export const fetchTaskFailed = createAction(
  "[Task API] Fetch Task Failed",
  props<{ error: any }>()
);

export const addTaskFormSubmitted = createAction(
  "[AddTask Page] AddTask  Form Submitted",
  props<{ task: Task }>()
);

export const editTaskFormSubmitted = createAction(
  "[EditTask Page] EditTask  Form Submitted",
  props<{ task: Task }>()
);

export const deleteTaskInitiated = createAction(
  "[DeleteTask Page] DeleteTask  Initiated",
  props<{ taskId: Task }>()
);
