namespace ContactManagement.Web.Api.AutoMapperConfiguration
{
    using AutoMapper;
    public class IncomingProfile : Profile
    {
        public IncomingProfile()
        {
            CreateMap<Contracts.Models.Contact, Domain.Models.Contact>(MemberList.Destination);
        }
    }
}
