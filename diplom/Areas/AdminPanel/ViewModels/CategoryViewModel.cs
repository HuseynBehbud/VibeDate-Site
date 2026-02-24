using System.ComponentModel.DataAnnotations;

namespace diplom.Areas.AdminPanel.ViewModels
{
    public class CategoryViewModel
    {
        [Required]
        public string Name { get; set; }
        
    }
}
