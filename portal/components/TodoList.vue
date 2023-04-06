<template>
  <div class="container mt-4">
    <h1 class="text-center mb-4">Tasks List</h1>

    <div class="row mb-4">
      <div class="container mt-4 d-flex justify-content-center">
        <b-form inline>
         
          <b-form-input
            id="todoName"
            class="mb-2 mr-sm-2 mb-sm-0"
            v-model="newTodo"
            placeholder="Task to do"
          ></b-form-input>

          <date-picker
            class="mb-2 mr-sm-2 mb-sm-0"
            v-model="todoDate"
            placeholder="Choose Date"
            format="DD/MM/YYYY"
          />
        
          <b-button
            @click="cancelUpdate"
            v-if="isUpdate"
            variant="danger"
            class="mb-2 mr-sm-2 mb-sm-0"
          >
            <font-awesome-icon icon="xmark"></font-awesome-icon> Cancel
          </b-button>
          <b-button
            v-if="isUpdate"
            @click="addTodo"
            variant="success"
            class="mb-2 mr-sm-2 mb-sm-0"
          >
            <font-awesome-icon icon="floppy-disk"></font-awesome-icon> Update
          </b-button>
          <b-button
            v-if="!isUpdate"
            :disabled="todoDate === '' || newTodo === ''"
            @click="addTodo"
            variant="primary"
            class="mb-2 mr-sm-2 mb-sm-0"
          >
            <font-awesome-icon icon="plus"></font-awesome-icon> Insert
          </b-button>
        </b-form>
      </div>
    </div>
    <b-form-checkbox v-model="mainCompletedCheckbox" v-show="todos.length" @change="toggleAllComplete">Mark all as complete</b-form-checkbox>
    <b-list-group>
      <b-list-group-item v-for="todo in todos" :key="todo.id">
        <div class="d-flex justify-content-between align-items-center">
          <div>
            <b-form-checkbox @change="toggleChildCheckbox" v-model="todo.completed"></b-form-checkbox>
          </div>
          <div>
            <h6 :class="todo.completed ? 'strike' : ''"> {{ todo.text }} {{ formatDate(todo.description) }}</h6>
           
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
    <b-badge v-if="todos.length" variant="primary">
      {{ todos.filter((f) => f.completed === false).length }} items
      left</b-badge
    >
    <b-badge v-if="todos.length" variant="secondary"
      >Clear {{ todos.filter((f) => f.completed).length }} completed
      item</b-badge
    >
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, useContext } from '@nuxtjs/composition-api'
import { Todo } from '~/types/todo'
import Swal from 'sweetalert2'
import { formatDate } from '~/helpers/common'
export default defineComponent({
  setup() {
    const newTodo = ref('')
    const todos = ref<Todo[]>([])
    const editingTodo = ref<Todo | null>(null)
    const todoDate = ref('')
    const isUpdate = ref(false)
    const completed = ref(false)
    const context = useContext()
    const mainCompletedCheckbox = ref(false)

    const toggleAllComplete = (e: boolean) => {      
        todos.value.forEach(f=> {
          f.completed = e
        })      
    }

    const toggleChildCheckbox = () => {
      mainCompletedCheckbox.value = todos.value.every(
        (option) => option.completed
      );
    }

    const addTodo = () => {
      const newId = todos.value.length + 1
      const newTodoItem = {
        id: newId,
        text: newTodo.value,
        description: todoDate.value,
        completed: completed.value,
      }
      todos.value.push(newTodoItem)
      newTodo.value = ''
      todoDate.value = ''
    }

    const editTodo = (todo: Todo) => {
      editingTodo.value = todo
      newTodo.value = todo.text
      todoDate.value = todo.description
      isUpdate.value = true
    }

    const saveTodo = () => {
      if (editingTodo.value) {
        editingTodo.value.text = newTodo.value
        editingTodo.value = null
        newTodo.value = ''
      }
    }

    const deleteTodo = (todo: Todo) => {
      Swal.fire({
        position: 'center',
        icon: 'error',
        text: `Delete task ${todo.text}?`,
        showConfirmButton: true,
        showCancelButton: true,
      })
    
    }

    const cancelUpdate = () => {
      isUpdate.value = false
      newTodo.value = ''
      todoDate.value = ''
    }

    return {
      newTodo,
      todos,
      editingTodo,
      todoDate,
      addTodo,
      editTodo,
      saveTodo,
      deleteTodo,
      isUpdate,
      cancelUpdate,
      formatDate,
      toggleAllComplete,
      mainCompletedCheckbox,
      toggleChildCheckbox
    }
  },
})
</script>
<style scoped>
.strike {
    text-decoration: Line-Through
}
</style>
