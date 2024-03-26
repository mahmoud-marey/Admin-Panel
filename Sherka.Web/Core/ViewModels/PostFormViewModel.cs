namespace Sherka.Web.Core.ViewModels
{
	public class PostFormViewModel
	{
        public int Id { get; set; }
        [MaxLength(500,ErrorMessage = Errors.MaxLength)]
        [Display(Name ="عنوان المقال")]
        public string Title { get; set; } = null!;
        [Display(Name = "محتويات المقال")]
        public string Body { get; set; } = null!;
        [Display(Name = "مقتطف")]
        public string Summary { get; set; } = null!;
        [Display(Name = "صورة المقال")]
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Thump { get; set; }
        public string? ThumpUrl { get; set; }
        [Display(Name = "وصف")]
        public string SEODiscription { get; set; } = null!;
        [MaxLength(500, ErrorMessage = Errors.MaxLength)]
        [Display(Name = "كلمات دلالية")]
        public string SEOKeyWords { get; set; } = null!;
        [MaxLength(300, ErrorMessage = Errors.MaxLength)]
        [Display(Name = "عنوان SEO")]
        public string SEOTitle { get; set; } = null!;
        public IEnumerable<string>? DiscriptionImagesString { get; set; } 
    }
}
