interface Todo {
    text: string;
    completed: boolean;
  }
  
  document.addEventListener('DOMContentLoaded', function() {
    const todoInput = document.getElementById('todoInput') as HTMLInputElement;
    const addBtn = document.getElementById('addBtn') as HTMLButtonElement;
    const todoList = document.getElementById('todoList') as HTMLDivElement;
    
    const storedTodos = localStorage.getItem('todos');
    let todos: Todo[] = storedTodos ? JSON.parse(storedTodos) : [];
    
    renderTodos();
    
    addBtn.addEventListener('click', addTodo);
    todoInput.addEventListener('keypress', function(e: KeyboardEvent) {
      if (e.key === 'Enter') addTodo();
    });
    
    todoInput.addEventListener('focus', function() {
      addBtn.classList.remove('bg-gray-400');
      addBtn.classList.add('bg-blue-500');
    });
    
    todoInput.addEventListener('blur', function() {
      if (!todoInput.value.trim()) {
        addBtn.classList.remove('bg-blue-500');
        addBtn.classList.add('bg-gray-400');
      }
    });
    
    todoInput.addEventListener('input', function() {
      if (this.value.trim()) {
        addBtn.classList.remove('bg-gray-400');
        addBtn.classList.add('bg-blue-500');
      } else if (document.activeElement !== todoInput) {
        addBtn.classList.remove('bg-blue-500');
        addBtn.classList.add('bg-gray-400');
      }
    });
    
    function addTodo(): void {
      const text = todoInput.value.trim();
      if (text) {
        todos.push({ text, completed: false });
        saveAndRender();
        todoInput.value = '';
        todoInput.focus();
      }
    }
    
    function renderTodos(): void {
      todoList.innerHTML = '';
      
      todos.forEach((todo: Todo, index: number) => {
        const todoItem = document.createElement('div');
        todoItem.className = 'flex items-center justify-between p-3 border-b hover:bg-gray-50 rounded';
        
        todoItem.innerHTML = `
          <span class="flex-1 ${todo.completed ? 'completed' : ''}">${todo.text}</span>
          <div class="flex items-center gap-2">
            <button 
              data-index="${index}"
              class="icon-btn ${todo.completed ? 'text-green-500' : 'text-gray-400 hover:text-green-500'}"
              title="${todo.completed ? 'Mark as incomplete' : 'Mark as complete'}"
            >
              <i class="fas fa-${todo.completed ? 'check-circle' : 'circle'}"></i>
            </button>
            <button 
              data-index="${index}"
              class="icon-btn text-red-400 hover:text-red-600"
              title="Delete task"
            >
              <i class="fas fa-trash-alt"></i>
            </button>
          </div>
        `;
        
        todoList.appendChild(todoItem);
      });
      
      document.querySelectorAll('#todoList button:nth-child(1)').forEach(btn => {
        btn.addEventListener('click', toggleTodo);
      });
      
      document.querySelectorAll('#todoList button:nth-child(2)').forEach(btn => {
        btn.addEventListener('click', deleteTodo);
      });
    }
    
    function toggleTodo(e: Event): void {
      const target = e.target as HTMLElement;
      const button = target.closest('button') as HTMLButtonElement;
      const index = button.dataset.index;
      if (index !== undefined) {
        todos[parseInt(index)].completed = !todos[parseInt(index)].completed;
        saveAndRender();
      }
    }
    
    function deleteTodo(e: Event): void {
      const target = e.target as HTMLElement;
      const button = target.closest('button') as HTMLButtonElement;
      const index = button.dataset.index;
      if (index !== undefined) {
        todos.splice(parseInt(index), 1);
        saveAndRender();
      }
    }
    
    function saveAndRender(): void {
      localStorage.setItem('todos', JSON.stringify(todos));
      renderTodos();
    }
  });