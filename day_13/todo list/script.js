document.addEventListener('DOMContentLoaded', function () {
    var todoInput = document.getElementById('todoInput');
    var addBtn = document.getElementById('addBtn');
    var todoList = document.getElementById('todoList');
    var storedTodos = localStorage.getItem('todos');
    var todos = storedTodos ? JSON.parse(storedTodos) : [];
    renderTodos();
    addBtn.addEventListener('click', addTodo);
    todoInput.addEventListener('keypress', function (e) {
        if (e.key === 'Enter')
            addTodo();
    });
    todoInput.addEventListener('focus', function () {
        addBtn.classList.remove('bg-gray-400');
        addBtn.classList.add('bg-blue-500');
    });
    todoInput.addEventListener('blur', function () {
        if (!todoInput.value.trim()) {
            addBtn.classList.remove('bg-blue-500');
            addBtn.classList.add('bg-gray-400');
        }
    });
    todoInput.addEventListener('input', function () {
        if (this.value.trim()) {
            addBtn.classList.remove('bg-gray-400');
            addBtn.classList.add('bg-blue-500');
        }
        else if (document.activeElement !== todoInput) {
            addBtn.classList.remove('bg-blue-500');
            addBtn.classList.add('bg-gray-400');
        }
    });
    function addTodo() {
        var text = todoInput.value.trim();
        if (text) {
            todos.push({ text: text, completed: false });
            saveAndRender();
            todoInput.value = '';
            todoInput.focus();
        }
    }
    function renderTodos() {
        todoList.innerHTML = '';
        todos.forEach(function (todo, index) {
            var todoItem = document.createElement('div');
            todoItem.className = 'flex items-center justify-between p-3 border-b hover:bg-gray-50 rounded';
            todoItem.innerHTML = "\n          <span class=\"flex-1 ".concat(todo.completed ? 'completed' : '', "\">").concat(todo.text, "</span>\n          <div class=\"flex items-center gap-2\">\n            <button \n              data-index=\"").concat(index, "\"\n              class=\"icon-btn ").concat(todo.completed ? 'text-green-500' : 'text-gray-400 hover:text-green-500', "\"\n              title=\"").concat(todo.completed ? 'Mark as incomplete' : 'Mark as complete', "\"\n            >\n              <i class=\"fas fa-").concat(todo.completed ? 'check-circle' : 'circle', "\"></i>\n            </button>\n            <button \n              data-index=\"").concat(index, "\"\n              class=\"icon-btn text-red-400 hover:text-red-600\"\n              title=\"Delete task\"\n            >\n              <i class=\"fas fa-trash-alt\"></i>\n            </button>\n          </div>\n        ");
            todoList.appendChild(todoItem);
        });
        document.querySelectorAll('#todoList button:nth-child(1)').forEach(function (btn) {
            btn.addEventListener('click', toggleTodo);
        });
        document.querySelectorAll('#todoList button:nth-child(2)').forEach(function (btn) {
            btn.addEventListener('click', deleteTodo);
        });
    }
    function toggleTodo(e) {
        var target = e.target;
        var button = target.closest('button');
        var index = button.dataset.index;
        if (index !== undefined) {
            todos[parseInt(index)].completed = !todos[parseInt(index)].completed;
            saveAndRender();
        }
    }
    function deleteTodo(e) {
        var target = e.target;
        var button = target.closest('button');
        var index = button.dataset.index;
        if (index !== undefined) {
            todos.splice(parseInt(index), 1);
            saveAndRender();
        }
    }
    function saveAndRender() {
        localStorage.setItem('todos', JSON.stringify(todos));
        renderTodos();
    }
});
