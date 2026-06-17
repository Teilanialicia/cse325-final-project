
namespace FocusFlow.Models;

public class StudentTask
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime DueDate { get; set; } = DateTime.UtcNow;
    public string Priority { get; set; } = "Medium";
    public bool IsCompleted { get; set; }
}