using System;
using todoCli.Models;
using todoCli.Data;
using todoCli.Services;
using todoCli.App;

namespace todoCli.App {
  public class TodoApp {

    private readonly ApplicationDbContext db;

    public TodoApp() {
      db = new ApplicationDbContext();
    }

    public void Run() {

      bool running = true;

      WelcomeMessage();

      ShowDoneAndTotalTodos();

      while(running) {

        Console.WriteLine("Add(a), Done(d), Remove(r), Show(s), Quit(q): ");    
        Console.WriteLine("----------------------------------------------");
        ConsoleKeyInfo key = Console.ReadKey(true);

        switch(key.KeyChar)
        {
          case 'a':
            AddTodoItem();
            break;

          case 'd':
            ToogleTodoDoneOption();
            break;

          case 'r':
            RemoveTodoItem();
            break;

          case 's':
            ShowTodoList();
            ShowDoneAndTotalTodos();
            break;

          case 'q':
            Console.WriteLine("Byeeeee!!!");
            running = false; // prekida while petlju
            break;

          default: 
            Console.WriteLine("Please enter wanted action");
            break;
        }
      }    
    } 

    public void WelcomeMessage() {
      Console.WriteLine("####################");
      Console.WriteLine("Welcome To Todo App!");
      Console.WriteLine("####################");
    }

    public void ShowDoneAndTotalTodos() {
      // VARIABLES
      int numberOfItems = 0;
      int completedTodo = 0; 

      var todoList = db.TodoItems.ToList();
      var todoService = new TodoService(todoList);

      // SUM OF ITEMS
      numberOfItems = todoService.itemCounter(numberOfItems);
      completedTodo = todoService.isDone(completedTodo);

      // SHOW NUMBER OF COMPLETED AND TOTAL NUMBER OD TASKS
      todoService.ShowDoneTasks(completedTodo, numberOfItems);
      Console.WriteLine("");
    }

    public void AddTodoItem() {

      var todoList = db.TodoItems.ToList();
      var todoService = new TodoService(todoList);

      var newItemTitle = todoService.UserInput("Enter item title: ");

      var todo = new TodoItem
      {
        Title = newItemTitle,
              IsDone = false
      };
      // add to database and save
      db.TodoItems.Add(todo);
      db.SaveChanges();
    }

    public void ToogleTodoDoneOption() {

      var todoList = db.TodoItems.ToList();
      var todoService = new TodoService(todoList);

      // show list with index
      todoService.ShowList(); 
      Console.WriteLine("");

      // check if there is any item in list
      if(todoList.Count() == 0) {
        Console.WriteLine("List is empty!");
      }
      else {
        // enter item index
        int itemDone = todoService.UserInputIntReturn
          ("Enter index of item to mark done: ");

        if(itemDone <= todoList.Count()) {
          todoList[itemDone - 1].IsDone = !todoList[itemDone - 1].IsDone; // ! će prebaciti na true ako je false i obrnuto

          // save changes
          db.SaveChanges(); 
        }
        else {
          Console.WriteLine("That item index does not exist!");
        }
      }
    }

    public void RemoveTodoItem() {

      var todoList = db.TodoItems.ToList();
      var todoService = new TodoService(todoList);

      // chech if list is empty
      if(todoList.Count() == 0) {
        Console.WriteLine("Can't remove, list is already empty");
      }
      else {
        // show items in list
        todoService.ShowList();
        Console.WriteLine("");

        // enter item index converted in int
        var itemRemove = todoService.UserInputIntReturn
          ("Enter index of item to remove: ");

        // update database and save changes
        db.TodoItems.Remove(todoList[itemRemove - 1]);
        db.SaveChanges();
      }
    }

    public void ShowTodoList() {
      var todoList = db.TodoItems.ToList();
      var todoService = new TodoService(todoList);

      todoService.ShowList();
      Console.WriteLine("");
    }
  }
}
