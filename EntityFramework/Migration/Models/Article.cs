using System.ComponentModel.DataAnnotations;

namespace MigrationPractice.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { set; get; }

        [StringLength(100)]
        public string Name { set; get; }
    }
}
