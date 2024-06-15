using AutoMapper;
using EmployeeManagementSystem.Entity;
using EmployeeManagementSystem.ModelDto;

namespace EmployeeManagementSystem.Comman
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {

            CreateMap<EmployeeAdditionalDetails,Model_IdentityInfo_>().ReverseMap();
            CreateMap<EmployeeAdditionalDetails,Model_PersonalDetails_>().ReverseMap();
            CreateMap<EmployeeAdditionalDetails,Model_WorkInfo_>().ReverseMap();
            CreateMap<EmployeeAdditionalDetails,ModelEmployeeAdditionalDetails>().ReverseMap();
            CreateMap<EmployeeBasicDetails, ModelEmployeeBasicDetails>().ReverseMap();

        }

    }
}
