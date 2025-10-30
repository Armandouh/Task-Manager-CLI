using System.Text.Json;

namespace TaskManagerCLI;

public class TaskManager
{
    private const string FileName = "tasks.json";
    private List<TaskItem> _tasks = new();

    public TaskManager()
    {
        Load();
    }

    private void Load()
    {
        if (!File.Exists(FileName))
            return;

        string json = File.ReadAllText(FileName);
        
        if (string.IsNullOrWhiteSpace(json))
            return;
        var loaded = JsonSerializer.Deserialize<List<TaskItem>>(json);
        if (loaded != null)
            _tasks = loaded;
    }

    private void Save()
    {
        string json = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions{WriteIndented = true});
        File.WriteAllText(FileName, json);
    }

    public void Add(string description)
    {
        int nextId = _tasks.Count == 0 ? 1 : _tasks.Max(t => t.Id) + 1;
        _tasks.Add(new TaskItem {Id = nextId, Description = description});
        Save();
        Console.WriteLine($"Added task #{nextId}");
    }

    public void List()
    {
        if (_tasks.Count == 0)
        {
            Console.WriteLine("No tasks yet!");
        }

        foreach (var task in _tasks)
        {
            Console.WriteLine(task);
        }
    }

    public void Done(int Id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == Id);
        if (task == null)
        {
            Console.WriteLine($"Task {Id} not found!");
            return;
        }

        task.isDone = true;
        Save();
        Console.WriteLine($"Task {Id} marked as done!");
    }

    public void Delete(int Id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == Id);
        if (task == null)
        {
            Console.WriteLine($"Task {Id} not found!");
            return;
        }

        _tasks.Remove(task);
        Save();
        Console.WriteLine($"Deleted task #{Id}");
    }
}