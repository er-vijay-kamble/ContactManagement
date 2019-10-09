namespace ContactManagement.Web.Client.Interfaces
{
    using ContactManagement.Contracts.Models;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IContactManagementService
    {
        #region Methods

        Task<HttpResponseMessage> GetContactByIdAsync(int id);
        Task<HttpResponseMessage> GetAllActiveContactsAsync(int pageIndex, int pageSize = 10);
        Task<HttpResponseMessage> GetAllInActiveContactsAsync(int pageIndex, int pageSize = 10);
        Task<HttpResponseMessage> AddContactAsync(Contact contact);
        Task<HttpResponseMessage> UpdateContactAsync(Contact contact);
        Task<HttpResponseMessage> DeleteContactAsync(int id);

        #endregion
    }
}
