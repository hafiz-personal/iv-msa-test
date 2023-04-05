<template>
  <div class="container mt-4">
    <h1 class="text-center mb-4">Tasks List</h1>

    <div class="row mb-4">
      <div class="container mt-4 d-flex justify-content-center">
        <b-form inline>
          <label class="sr-only" for="todoName">Name</label>
          <b-form-input
            id="todoName"
            class="mb-2 mr-sm-2 mb-sm-0"
            v-model="newTodo"
            placeholder="Task to do"
          ></b-form-input>

          <label class="sr-only" for="todoDate">Date</label>
          <b-form-input
            id="todoDate"
            class="mb-2 mr-sm-2 mb-sm-0"
            v-model="todoDescription"
            placeholder="Date"
          ></b-form-input>

          <b-button @click="addTodo" variant="primary">
            <font-awesome-icon icon="plus"></font-awesome-icon>
          </b-button>
        </b-form>
      </div>
    </div>
    <b-list-group>
      <b-list-group-item v-for="todo in todos" :key="todo.id">
        <div class="d-flex justify-content-between align-items-center">
          <div>
            <h5>{{ todo.text }}</h5>
            <small>{{ todo.description }}</small>
          </div>
          <div>
            <b-button @click="editTodo(todo)" variant="secondary">
              <font-awesome-icon icon="pencil"></font-awesome-icon>
            </b-button>
            <b-button @click="deleteTodo(todo)" variant="danger">
              <font-awesome-icon icon="trash"></font-awesome-icon>
            </b-button>
          </div>
        </div>
      </b-list-group-item>
    </b-list-group>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from '@nuxtjs/composition-api'
import { Todo } from '~/types/todo'

export default defineComponent({
  setup() {
    const newTodo = ref('')
    const todos = ref<Todo[]>([])
    const editingTodo = ref<Todo | null>(null)
    const todoDescription = ref('')

    const addTodo = () => {
      const newId = todos.value.length + 1
      const newTodoItem = {
        id: newId,
        text: newTodo.value,
        description: todoDescription.value,
      }
      todos.value.push(newTodoItem)
      newTodo.value = ''
    }

    const editTodo = (todo: Todo) => {
      editingTodo.value = todo
      newTodo.value = todo.text
    }

    const saveTodo = () => {
      if (editingTodo.value) {
        editingTodo.value.text = newTodo.value
        editingTodo.value = null
        newTodo.value = ''
      }
    }

    const deleteTodo = (todo: Todo) => {
      todos.value = todos.value.filter((item) => item.id !== todo.id)
    }

    return {
      newTodo,
      todos,
      editingTodo,
      todoDescription,
      addTodo,
      editTodo,
      saveTodo,
      deleteTodo,
    }
  },
})
</script>
