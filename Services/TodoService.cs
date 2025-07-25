using todoCli.Data;
using todoCli.Models;

namespace todoCli.Services {

  public class TodoService {

    private List<TodoItem> _todoList;

    public TodoService(List<TodoItem> todoList) {
      _todoList = todoList;
    }

    // RETURN USER INPUT
    public string UserInput(string message) {
      Console.Write(message);
      return Console.ReadLine();  
    }

    // RETURN USER INPUT IN INT
    public int UserInputIntReturn(string message) {
      Console.Write(message);
      var ParseToNumber = int.Parse(Console.ReadLine());
      return ParseToNumber;
    }

    // PRINT LIST OF ITEMS
    public void ShowList() {
      for(int i = 0; i < _todoList.Count; i++) {
        Console.WriteLine
          ($"{i + 1} - {_todoList[i].Title} --> {_todoList[i].IsDone}");  
      }
    }

    // SUM OF ITEMS
    public int itemCounter(int numberOfItems) {
      foreach(var item in _todoList) {
        numberOfItems++;
      } 
      return numberOfItems;
    }

    // SUM OF COMPLETED ITEMS
    public int isDone(int completedTodo) {
      foreach(var todo in _todoList) {
        if(todo.IsDone == true) {
          completedTodo++;
        }
      }
      return completedTodo;
    }

    // SHOW NUMBER OF COMPLETED AND TOTAL NUMBER OD TASKS
    public void ShowDoneTasks(int numberOfItems, int completedTodo) {
       Console.WriteLine($"Completed tasks: {numberOfItems}/{completedTodo}"); 
      }
  }
}










