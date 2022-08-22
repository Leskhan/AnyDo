using Entities.Abstract;

namespace Entities
{
    public class TaskEntity : BaseEntity
    {
        public string? EndDate { get; set; }
        public string? Notes { get; set; }
        public string CreatedDate { get; set; }

        public int ListEntityId { get; set; }
        public ListEntity List { get; set; }
    }
}
