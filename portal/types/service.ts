import { Todo } from "./todo"

export interface Service {
    todo: {
        get: () => Promise<Todo[]>;
    }
}