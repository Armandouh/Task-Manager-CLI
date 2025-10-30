namespace TaskManagerCLI;

public class TaskItem
{
    public int Id { get; set; }
    public string Description { get; set; } = "";
    public bool isDone { get; set; } = false;

    public override string ToString()
    {
        string status = isDone ? "[\u2713]" : "[]";
        return $"{Id}. {status} {Description}";
    }
}