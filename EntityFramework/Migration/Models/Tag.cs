using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Migration.Models
{
    public class Tag
    {
        [Key]
        [StringLength(20)]
        public string TagId { set; get; }
        [Column(TypeName = "nvarchar(max)")]
        public string Content { set; get; }
    }
}
