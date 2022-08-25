using System.ComponentModel.DataAnnotations;

namespace AnyDo.ViewModels
{
    public class ListViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
