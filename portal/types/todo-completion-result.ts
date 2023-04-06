import { TodoCompletionRequest } from "./todo-completion-request";

export interface TodoCompletionResult extends TodoCompletionRequest {
    isSuccess: boolean,
}