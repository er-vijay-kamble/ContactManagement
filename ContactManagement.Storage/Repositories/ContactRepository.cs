namespace ContactManagement.Storage.Repositories
{
    using ContactManagement.Domain.Models;
    using ContactManagement.Storage.DbConfiguration;
    using ContactManagement.Storage.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ContactRepository : Repository<Contact>, IContactRepository, IDisposable
    {
        #region Fields
        private readonly ManagementContext _managementContext;
        #endregion

        #region Constructors

        public ContactRepository(ManagementContext managementContext) :
            base(managementContext)
        {
            _managementContext = managementContext;
        }

        #endregion

        #region Methods
        public async Task<ICollection<Contact>> GetAllActiveContactsAsync(int pageIndex, int pageSize = 10)
        {
            return await Task.FromResult(
                _managementContext.Contacts
                .Where(contact => contact.IsActive)
                .OrderBy(contact => contact.ContactId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                );
        }

        public async Task<ICollection<Contact>> GetAllInActiveContactsAsync(int pageIndex, int pageSize = 10)
        {
            return await Task.FromResult(
                 _managementContext.Contacts
                .Where(contact => !contact.IsActive)
                .OrderBy(contact => contact.ContactId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                );
        }

        #endregion

        #region Dispose

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                base.Dispose();
                _managementContext.Dispose();
            }
        }
        #endregion
    }
}
