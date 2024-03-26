using AutoMapper;

namespace Sherka.Web.Core.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<PostFormViewModel, Post>().ReverseMap();
			CreateMap<AdminHomeViewModel, AdminHome>().ReverseMap();
			CreateMap<ContactViewModel, Contact>().ReverseMap();
			CreateMap<PhotoViewModel, Photo>().ReverseMap();
			CreateMap<AboutViewModel, About>().ReverseMap();
			CreateMap<Post, PostViewModel>();
			CreateMap<Post, ViewPostViewModel>();

            CreateMap<ApplicationUser, UserViewModel>();
        }
	}
}
