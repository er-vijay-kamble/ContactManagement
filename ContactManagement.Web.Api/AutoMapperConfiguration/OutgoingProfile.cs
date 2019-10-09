namespace ContactManagement.Web.Api.AutoMapperConfiguration
{
    using AutoMapper;
    public class OutgoingProfile : Profile
    {
        public OutgoingProfile()
        {
            CreateMap<Domain.Models.Contact, Contracts.Models.Contact>(MemberList.Destination);
        }
    }
}
