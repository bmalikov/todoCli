using todoCli.Models;
using todoCli.Data;

Console.WriteLine("Welcome to ToDo application: ");

Console.WriteLine("Add(a), Remove(r), Show(s), Quit(q)");
var userInput = Console.ReadLine(); 

var db = new ApplicationDbContext();

while(true) {

  switch(userInput) {
    case "a":
      Console.WriteLine("Enter item title: ");
      var item = Console.ReadLine();

      var newItem = new TodoItem
      {
        Title = item,
        IsDone = false
      };

      db.TodoItems.Add(newItem);
      db.SaveChanges();
      break;

    case "r":
      Console.WriteLine("REMOVEEEE");
      break;

    case "s":
      var todoList = db.TodoItems.ToList();
      foreach(var todo in todoList) {
        Console.WriteLine($"{todo.Id} - {todo.Title}");
      }
      break;

    case "q":
      Console.WriteLine("QUITTTT");
      break;

    default:
      Console.WriteLine("Unknown command!");
      break;
  }
}
