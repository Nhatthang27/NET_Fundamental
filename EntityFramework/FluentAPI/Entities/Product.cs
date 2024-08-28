using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentAPI.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")] //đặt tên cho trường khóa ngoại trong bảng -> tên tùy ý -> nếu không có sẽ tự động là CategoryId (tên key của bản pricipal)
        public virtual Category Category { get; set; }
        public override string ToString()
        {

            return $"Id: {Id}, Name: {Name}, Price: {Price}, Stock: {Stock}, Category: {(Category != null ? Category.Name : "null null null")}, CategoryId: {CategoryId}";
        }

    }
}
