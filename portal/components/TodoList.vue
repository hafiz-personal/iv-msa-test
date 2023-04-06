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
            :disabled="todoDate === null || newTodo === ''"
            @click="addTodo"
            variant="primary"
            class="mb-2 mr-sm-2 mb-sm-0"
          >
            <font-awesome-icon icon="plus"></font-awesome-icon> Insert
          </b-button>
        </b-form>
      </div>
    </div>
    <b-form-checkbox
      v-model="mainCompletedCheckbox"
      v-show="todos.length"
      @change="toggleAllComplete"
      >Mark all as complete</b-form-checkbox
    >
    <b-list-group>
      <b-list-group-item v-for="todo in todos" :key="todo.id">
        <div class="d-flex justify-content-between align-items-center">
          <div>
            <b-form-checkbox
              @change="toggleChildCheckbox(todo)"
              v-model="todo.done"
            ></b-form-checkbox>
          </div>
          <div>
            <h6 :class="todo.done ? 'strike' : ''">{{ todo.fulltext }}</h6>
          </div>
          <div>
            <!-- <b-button @click="editTodo(todo)" variant="secondary">
              <font-awesome-icon icon="pencil"></font-awesome-icon>
            </b-button> -->
            <b-button @click="deleteTodo(todo)" variant="danger">
              <font-awesome-icon icon="trash"></font-awesome-icon>
            </b-button>
          </div>
        </div>
      </b-list-group-item>
    </b-list-group>
    <b-badge v-if="todos.length" variant="primary">
      {{ todos.filter((f) => f.done === false).length }} items left</b-badge
    >
    <b-badge v-if="todos.length" variant="secondary"
      >Clear {{ todos.filter((f) => f.done).length }} completed item</b-badge
    >
  </div>
</template>

<script lang="ts">
import {
  defineComponent,
  ref,
  useContext,
  onMounted,
} from '@nuxtjs/composition-api'
import { Todo } from '~/types/todo'
import Swal from 'sweetalert2'
import { formatDate, formatDateToStr } from '~/helpers/common'
import todoService from '~/services/todo-service'
import { TodoCompletionRequest } from '~/types/todo-completion-request'
import { TodoCreate } from '~/types/todo-create'

export default defineComponent({
  setup() {
    const newTodo = ref('')
    const todos = ref<Todo[]>([])
    const editingTodo = ref<Todo>()
    const todoDate = ref<Date | null>(null)
    const isUpdate = ref(false)
    const ctx = useContext()
    const mainCompletedCheckbox = ref(false)
    
    
    const getAllTasks = async () => {
       todos.value = await todoService(ctx.$axios).get()
       toggleMainCheckboxFromChild()
    }

    const toggleAllComplete = async (e: boolean) => {
      todos.value.forEach((f) => {
        f.done = e
      })
      let result: TodoCompletionRequest[] = []
      let input = todos.value.map(m=> {
        result.push({
          id: m.id,
          isComplete: m.done,          
        } as TodoCompletionRequest)
      })
      await todoService(ctx.$axios).completeTask(result)
    }

    const toggleMainCheckboxFromChild = () => {
      mainCompletedCheckbox.value = todos.value.every((option) => option.done)
    }

    const toggleChildCheckbox = async (e: Todo) => {
      toggleMainCheckboxFromChild()
      let input = {
        id: e.id,
        isComplete: e.done
      } as TodoCompletionRequest
      let inputs: TodoCompletionRequest[] = []
      inputs.push(input)
      await todoService(ctx.$axios).completeTask(inputs)
    }

    const addTodo = async () => {      
    
     let input = {
        date: formatDateToStr(todoDate.value),
        text: newTodo.value
      } as TodoCreate
    
      await todoService(ctx.$axios).create(input)
      newTodo.value = ''
      todoDate.value = null
      await getAllTasks()
    }

    const editTodo = (todo: Todo) => {
      newTodo.value = todo.text
      todoDate.value = todo.date
      editingTodo.value = todo
    
    }

    const saveTodo = async (todo: Todo) => {
      await todoService(ctx.$axios).update(editingTodo.value!)
    }

    const deleteTodo = async (todo: Todo) => {
      let result = await Swal.fire({
        position: 'center',
        icon: 'error',
        text: `Delete task ${todo.text}?`,
        showConfirmButton: true,
        showCancelButton: true,
      })

      if(result.isConfirmed) {
        await todoService(ctx.$axios).delete(todo.id)
        await getAllTasks()
      }
    }

    const cancelUpdate = () => {
      isUpdate.value = false
      newTodo.value = ''
      todoDate.value = null
    }

    onMounted(async () => {
      await getAllTasks()
      
    })

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
      toggleChildCheckbox,
    }
  },
})
</script>
<style scoped>
.strike {
  text-decoration: Line-Through;
}
</style>
