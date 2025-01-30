using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_BookShop.Models.Models
{
    public class Category
    {

        [Key]/*identifies that this is the table's primary key*/
        public int CategoryId { get; set; }

        [MaxLength(30)]
        [DisplayName("Category Name")]/*Can be used to add validation*/
        [Required]/*states that a field is required*/
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
