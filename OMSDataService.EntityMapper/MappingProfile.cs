using AutoMapper;
using System;

namespace OMSDataService.EntityMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<ClassificationRatingItem, ClassificationRatingItemViewModel>()
            //        .ForMember(dest => dest.GroupName,
            //           opt => opt.MapFrom(src => src.ClassificationRatingSubGroup.ClassificationRatingGroup.Description))
            //        .ForMember(dest => dest.SubGroupName,
            //           opt => opt.MapFrom(src => src.ClassificationRatingSubGroup.Description))
            //       .ReverseMap();
        }
    }
}