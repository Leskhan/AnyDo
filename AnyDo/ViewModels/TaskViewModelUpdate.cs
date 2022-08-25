using System.ComponentModel.DataAnnotations;

namespace AnyDo.ViewModels
{
    public class TaskViewModelUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Notes { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCompleted { get; set; }
        public int ListModelId { get; set; }
    }
}
