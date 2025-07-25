using todoCli.Models;
using todoCli.Data;
using todoCli.Services;

Console.WriteLine("Welcome to ToDo application: ");

var db = new ApplicationDbContext();

while(true) {
  // VARIABLES
  var todoList = db.TodoItems.ToList();
  var todoService = new TodoService(todoList);
  int numberOfItems = 0;
  int completedTodo = 0; 

  // SUM OF ITEMS
  numberOfItems = todoService.itemCounter(numberOfItems);
  completedTodo = todoService.isDone(completedTodo);

  // SHOW NUMBER OF COMPLETED AND TOTAL NUMBER OD TASKS
  todoService.ShowDoneTasks(completedTodo, numberOfItems);

  // ASK USER TO CHOOSE ACTION
  var userInput = todoService.UserInput
    ("Add(a), Remove(r), Done(d), Show(s), Quit(q):");

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

    // enter item index
    var itemDone = todoService.UserInputIntReturn
      ("Enter index of item to mark done: ");

    // add true to item
    todoList[itemDone].IsDone = true;

    // save sum of completed todos
    completedTodo = todoService.isDone(completedTodo);

    // save changes
    db.SaveChanges();
  }

  // IF REMOVE
  else if(userInput == "remove") {
    // show items in list
    todoService.ShowList();

    // enter item index converted in int
    var itemRemove = todoService.userInputIntReturn
      ("Enter index of item to remove: ");

    // update database and save changes
    db.TodoItems.Remove(todoList[itemToDelete - 1]);
    db.SaveChanges();
  }

  // IF SHOW
  else if(userInput == "show") {
    // show items in list
    todoService.ShowList();
  }

  // IF QUIT
  else if(userInput == "quit") {
    Console.WriteLine("BYEEEE");
    break;
  }
}
