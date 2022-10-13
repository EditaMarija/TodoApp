namespace TodoApp.Model
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public DateTime? DoneDate { get; set; }

        public Todo()
        {
            Id = Guid.NewGuid();
        }

        public Todo(string text) : this()
        {
            Text = text;
        }
    }
}
