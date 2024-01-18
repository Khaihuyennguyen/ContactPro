using System.ComponentModel.DataAnnotations; //Provides attribute classes that are used to define metadata for ASP.NET MVC and ASP.NET data controls.

namespace ContactPro.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string? AppUserId { get; set; }

        [Required]
        [Display(Name="Category Name")]
        public string? Name { get; set; }

        // Virtuals
        public virtual AppUser? AppUser { get; set; } // create foreign keys
    }
}
