﻿namespace ContactManagement.Storage.Interfaces
{
    using ContactManagement.Domain.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IContactRepository : IRepository<Contact>
    {
        #region Methods

        Task<IEnumerable<Contact>> GetAllContactsAsync();
        Task<IEnumerable<Contact>> GetAllActiveContactsAsync(int pageIndex, int pageSize = 10);
        Task<IEnumerable<Contact>> GetAllInActiveContactsAsync(int pageIndex, int pageSize = 10);

        #endregion
    }
}
