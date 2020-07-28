using AutoMapper;
using PodiumCaseStudy.Models;
using PodiumCaseStudy.Services.Mortgage.Model;
using PodiumCaseStudy.Services.User;

namespace PodiumCaseStudy.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterModel, UserDto>();

            CreateMap<UserDto, Context.User>()
                .ForMember(x => x.Id, opt => opt.Ignore());


            CreateMap<MortgageProductQuery, MortgageProductQueryDto>();

            CreateMap<Context.MortgageProduct, MortgageProductDto>();

            CreateMap<MortgageProductDto, MortgageProduct>()
                .ForMember(x => x.MortgageType, opt => opt.MapFrom(x => x.MortgageType.ToString()));
        }
    }
}
