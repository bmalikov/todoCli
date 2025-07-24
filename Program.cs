using todoCli.Models;
using todoCli.Data;

Console.WriteLine("Welcome to ToDo application: ");

var db = new ApplicationDbContext();

while(true) {
  var todoList = db.TodoItems.ToList();

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

    for(var i = 0; i < todoList.Count; i++) {
      Console.WriteLine($"{i + 1} - {todoList[i].Title}");
    }

    Console.WriteLine("Enter index of item to remove: ");
    int itemToDelete = int.Parse(Console.ReadLine());

    db.TodoItems.Remove(todoList[itemToDelete - 1]);
    db.SaveChanges();

  }
  else if(userInput == "show") {

    for(var i = 0; i < todoList.Count; i++) {
      Console.WriteLine($"{i + 1} - {todoList[i].Title}");
    }
  }
  else if(userInput == "quit") {
    Console.WriteLine("BYEEEE");
    break;
  }
}
