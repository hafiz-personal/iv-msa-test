import { Context } from '@nuxt/types';
import { NuxtAxiosInstance } from '@nuxtjs/axios';
import { Todo } from "~/types/todo";
import { TodoCompletionRequest } from '~/types/todo-completion-request';
import { TodoCompletionResult } from '~/types/todo-completion-result';

export default (axios: NuxtAxiosInstance) => ({
    
    async get(): Promise<Todo[]> {        
        let data = await axios.$get<Todo[]>(`todo`)        
        return data
    },

    async completeTask(input: TodoCompletionRequest[]): Promise<TodoCompletionResult[]> {        
        let data = await axios.$post<TodoCompletionResult[]>(`todo/complete`, input)        
        return data
    }
})