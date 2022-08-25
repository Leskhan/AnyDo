namespace AnyDo.ViewModels
{
    public class TaskViewModel
    {
        public string Name { get; set; }
        public string? Notes { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ListModelId { get; set; }
    }
}
