using TaskManagerCLI;

var manager = new TaskManager();

if (args.Length == 0)
{
    Console.WriteLine("Commands: add <description>, list, done <Id>, delete <Id>");
}

string command = args[0].ToLower();

switch (command)
{
    case "add":
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: add <description>");
            return;
        }

        string description = string.Join(" ", args.Skip(1));
        manager.Add(description);
        break;
    
    case "list":
        manager.List();
        break;
    
    case "done":
        if (args.Length < 2 || !int.TryParse(args[1], out int doneId))
        {
            Console.WriteLine("Usage: done <Id>");
            return;
        } 
        manager.Done(doneId);
        break;
    
    case "delete":
        if (args.Length < 2 || !int.TryParse(args[1], out int deleteId))
        {
            Console.WriteLine("Usage: done <Id>");
            return;
        } 
        manager.Delete(deleteId);
        break;
    
    default:
        Console.WriteLine($"Unknown command '{command}'");
        break;
}