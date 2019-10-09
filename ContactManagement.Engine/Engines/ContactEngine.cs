namespace ContactManagement.Engine.Engines
{
    using ContactManagement.Domain.Interfaces;
    using ContactManagement.Domain.Models;
    using ContactManagement.Storage.Interfaces;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ContactEngine : IContactEngine
    {
        #region Fields

        private readonly IContactRepository _contactRepository;

        #endregion

        #region Constructors

        public ContactEngine(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        }


        #endregion

        #region "Public methods"

        public async Task<bool> AddContactAsync(Contact contact)
        {
            try
            {
                return await _contactRepository.InsertAsync(contact).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Information("{Engine} - {Method} - Unable to add cantact. Error: {exception}", nameof(ContactEngine), nameof(AddContactAsync), ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            try
            {
                return await _contactRepository.DeleteAsync(id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Information("{Engine} - {Method} - Unable to delete cantact. Error: {exception}", nameof(ContactEngine), nameof(DeleteContactAsync), ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<bool> UpdateContactAsync(Contact contact)
        {
            try
            {
                return await _contactRepository.UpdateAsync(contact).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Information("{Engine} - {Method} - Unable to update cantact. Error: {exception}", nameof(ContactEngine), nameof(UpdateContactAsync), ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            try
            {
                return await _contactRepository.GetByIdAsync(id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Information("{Engine} - {Method} - Unable to get cantact. Error: {exception}", nameof(ContactEngine), nameof(GetContactByIdAsync), ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<Contact>> GetAllActiveContactsAsync(int pageIndex, int pageSize = 10)
        {
            try
            {
                return await _contactRepository.GetAllActiveContactsAsync(pageIndex: pageIndex, pageSize: pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Information("{Engine} - {Method} - Unable to get active cantacts. Error: {exception}", nameof(ContactEngine), nameof(GetAllActiveContactsAsync), ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<Contact>> GetAllInActiveContactsAsync(int pageIndex, int pageSize = 10)
        {
            try
            {
                return await _contactRepository.GetAllInActiveContactsAsync(pageIndex: pageIndex, pageSize: pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Information("{Engine} - {Method} - Unable to get in-active cantacts. Error: {exception}", nameof(ContactEngine), nameof(GetAllInActiveContactsAsync), ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion
    }
}
