using todoCli.Data;
using todoCli.Models;

namespace todoCli.Services {

  public class TodoService {

    private List<TodoItem> _todoList;

    public TodoService(List<TodoItem> todoList) {
      _todoList = todoList;
    }

    public void ShowList() {

      for(int i = 0; i < _todoList.Count; i++) {
        Console.WriteLine($"{i + 1} - {_todoList[i].Title}");  
      }
    }
  }
}
