using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace diplom.Models
{
    public class Products 
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

    }
}
