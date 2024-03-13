import { HttpErrorResponse } from "@angular/common/http";

export interface IHttpResponsesHandler {
    handleSuccess?(): void;
    handleError?(errorResponse: HttpErrorResponse): void;
}

export interface IHttpResponseHandlerSettings {
    showSuccessMessage?: boolean; // Default false
    successMessage?: string;
    showErrorMessage?: boolean; // Default true
    errorMessage?: string;
    errorStatusesMessages?: {[key: number]: string};
}
