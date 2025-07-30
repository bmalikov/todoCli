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
      WelcomeMessage();

      TodoCounter();

      while(true) {
        var userInput = DisplayMainMenuAndGetInput();

        var todoList = db.TodoItems.ToList();
        var todoService = new TodoService(todoList);

        // IF ADD
        if(userInput == "add") {

          // new item title
          var newItem = todoService.UserInput
            ("Enter item title: ");

          // instance new object
          var todo = new TodoItem 
          {
            Title = newItem,
            IsDone = false
          };

          // add to database and save
          db.TodoItems.Add(todo);
          db.SaveChanges();
        } 
        // IF DONE
        else if(userInput == "done") {

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
              var key = Console.ReadKey(true).Key; // čita pritisnutu tipku i sprema u varijablu, true znači da ne ispisuje tipku na ekran  

              if(key == ConsoleKey.Spacebar) {
                // add true to item
                todoList[itemDone - 1].IsDone = !todoList[itemDone - 1].IsDone; // ! će prebaciti na true ako je false i obrnuto
              }

              // save changes
              db.SaveChanges(); 
            }
            else {
              Console.WriteLine("That item index does not exist!");
            }
          }
        }
        // IF REMOVE
        else if(userInput == "remove") {
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
        // IF SHOW
        else if(userInput == "show") {
          // show items in list
          todoService.ShowList();
          Console.WriteLine("");

          TodoCounter();
        }
        // IF QUIT
        else if(userInput == "quit") {
          Console.WriteLine("BYEEEE");
          break;
        }
        else {
          Console.WriteLine("Please enter wanted action");
        }
      }    
    } // === Run() ===

    public void WelcomeMessage() {
      Console.WriteLine("###################");
      Console.WriteLine("Welcome to ToDo App");
      Console.WriteLine("###################");
    }

    public string DisplayMainMenuAndGetInput() {

      var todoList = db.TodoItems.ToList();
      var todoService = new TodoService(todoList);

      var userInput = todoService.UserInput
        ("Add(a), Remove(r), Done(d), Show(s), Quit(q):");
      Console.WriteLine
        ("---------------------------------------------");
      Console.WriteLine("");
      return userInput;
    }

    public void TodoCounter() {
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
  }
}
