using Customer.Management.API.Models.Authentication.SignUp;
using CustomerManagement.Models.Authentication.SignUp;

namespace Customer.Management.API.Mappings
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterAddEditModel, Register>().ReverseMap();
        }

    }
}
