namespace Sherka.Web.Core.Models
{
    public class Post
    {
        public int Id { get; set; }
        [MaxLength(500)]
        public string Title { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string? ThumpUrl { get; set; }
        [MaxLength(300)]
        public string SEOTitle { get; set; } = null!;
        [MaxLength(500)]
        public string SEOKeyWords { get; set; } = null!;
        public string SEODiscription { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;



    }
}
