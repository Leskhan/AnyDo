namespace AnyDo.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Notes { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsCompleted { get; set; }

        public int? ListModelId { get; set; }
        public ListModel? List { get; set; }
    }
}
