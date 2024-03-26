

namespace Sherka.Web.Core.ViewModels
{
    public class PhotoViewModel
    {
        public int Id { get; set; }
        [Display (Name ="Image Name")]
        public string ImgName { get; set; } = null!;
        public string? ImgUrl { get; set; } 
        public IFormFile? Image { get; set; }

    }
}
