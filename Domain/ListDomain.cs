namespace Domain
{
    public class ListDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskDomain> Tasks { get; set; }
    }
}
