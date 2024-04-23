namespace todo_list.api.Entities
{
    public class TODO
    {
        public int Id { get; set; }
        public TaskList TaskList { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public bool IsDone { get; set; }
        public bool IsDeleted { get; set; }
    }
}
