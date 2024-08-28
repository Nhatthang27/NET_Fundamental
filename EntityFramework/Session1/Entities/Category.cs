using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Session1.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }


        //Collection Navigation 
        public virtual List<Product> Products { get; set; }
    }
}
