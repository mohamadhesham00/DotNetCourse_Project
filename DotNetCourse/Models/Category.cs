using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNetCourse.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Category Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1,100)]
        public int DisplayOrder { get; set; }
    }
}
