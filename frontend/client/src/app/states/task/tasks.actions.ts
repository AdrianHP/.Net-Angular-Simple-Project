import { createAction, props } from '@ngrx/store';
import { Task } from 'src/app/project/tasks/interfaces/task';

//#region Fetch Actions
export const appLoaded = createAction('[App] App Loaded');

export const fetchTaskSuccess = createAction(
  '[Task API] Fetch Task Success',
  props<{ tasks: Task[] }>()
);

export const fetchTaskFailed = createAction(
  '[Task API] Fetch Task Failed',
  props<{ error: any }>()
);
//#endregion

//#region Add Actions
export const addTaskFormSubmitted = createAction(
  '[AddTask Page] AddTask  Form Submitted',
  props<{ task: Task }>()
);

export const addTaskSuccess = createAction('[Task API] Add Task Item Success');

export const addTaskFailed = createAction(
  '[Task API] Add Task Item Failed',
  props<{ error: any }>()
);
//#endregion

//#region Edit Actions
export const editTaskFormSubmitted = createAction(
  '[EditTask Page] EditTask  Form Submitted',
  props<{ task: Task }>()
);

export const editTaskSuccess = createAction(
  '[Task API] Edit Task Item Success',
  props<{ Task: Task }>()
);

export const editTaskFailed = createAction(
  '[Task API] Edit Task Item Failed',
  props<{ error: any }>()
);
//#endregion

//#region Delete Actions
export const deleteTaskInitiated = createAction(
  '[DeleteTask Page] DeleteTask  Initiated',
  props<{ taskId: Task }>()
);

export const deleteTaskSuccess = createAction(
  '[Task API] Delete Task Item Success',
  props<{ TaskId: string }>()
);

export const deleteTaskFailed = createAction(
  '[Task API] Delete Task Item Failed',
  props<{ error: any }>()
);
//#endregion
