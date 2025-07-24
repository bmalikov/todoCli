using todoCli.Models;
using todoCli.Data;
using todoCli.Services;

Console.WriteLine("Welcome to ToDo application: ");

var db = new ApplicationDbContext();

while(true) {
  var todoList = db.TodoItems.ToList();
  var todoService = new TodoService(todoList);

  todoService.ShowList();

  Console.Write("Add(a), Remove(r), Show(s), Quit(q): ");
  var userInput = Console.ReadLine(); 

  if(userInput == "add") {
    Console.Write("Enter new item: ");
    var newItem = Console.ReadLine();

    var item = new TodoItem {Title = newItem};

    db.TodoItems.Add(item);
    db.SaveChanges();
  } 
  else if(userInput == "remove") {
    todoService.ShowList();

    Console.WriteLine("Enter index of item to remove: ");
    int itemToDelete = int.Parse(Console.ReadLine());

    db.TodoItems.Remove(todoList[itemToDelete - 1]);
    db.SaveChanges();

  }
  else if(userInput == "show") {
    todoService.ShowList();
  }
  else if(userInput == "quit") {
    Console.WriteLine("BYEEEE");
    break;
  }
}
