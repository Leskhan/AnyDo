namespace Domain
{
    public class TaskDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Notes { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCompleted { get; set; }

        public int ListDomainId { get; set; }
        public ListDomain List { get; set; }
    }
}
