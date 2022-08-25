using System.ComponentModel.DataAnnotations;

namespace AnyDo.ViewModels
{
    public class ListViewModelUpdate
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
