public class TodoItem // a class built for 'Todo.razor' that creates Todo list item objects
{   // gets specified value from user input and assigns it to 'Title'
    public string? Title { get; set; } // warning says to consider setting variable as nullable
    // not currently referenced
    public bool IsDone { get; set; }
}
