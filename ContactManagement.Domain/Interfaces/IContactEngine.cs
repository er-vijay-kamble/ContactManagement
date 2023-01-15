namespace ContactManagement.Domain.Interfaces
{
    using ContactManagement.Domain.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IContactEngine
    {
        #region Methods

        Task<Contact> GetContactByIdAsync(int id);
        Task<IEnumerable<Contact>> GetAllContactsAsync();
        Task<IEnumerable<Contact>> GetAllActiveContactsAsync(int pageIndex, int pageSize = 10);
        Task<IEnumerable<Contact>> GetAllInActiveContactsAsync(int pageIndex, int pageSize = 10);
        Task<bool> AddContactAsync(Contact contact);
        Task<bool> UpdateContactAsync(Contact contact);
        Task<bool> DeleteContactAsync(int id);

        #endregion
    }
}
