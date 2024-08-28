using System.ComponentModel.DataAnnotations;

namespace Migration.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { set; get; }

        [StringLength(100)]
        public string Title { set; get; }
    }
}
