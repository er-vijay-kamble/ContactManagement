namespace ContactManagement.Web.Api.Controllers
{
    using AutoMapper;
    using ContactManagement.Contracts.Models;
    using ContactManagement.Domain.Exceptions;
    using ContactManagement.Domain.Interfaces;
    using ContactManagement.Web.Api.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactEngine _contactEngine;
        private readonly IMapper _mapper;

        /// <summary>
        /// Api for managing contacts
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="contactEngine"></param>
        public ContactController(IMapper mapper, IContactEngine contactEngine)
        {
            _contactEngine = contactEngine ?? throw new ArgumentNullException(nameof(contactEngine));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #region "Public methods"

        /// <summary>
        /// Retrieves all active contacts
        /// </summary>
        /// <returns>Contacts</returns>
        /// <response code="200">Successfully retrieved active Contacts</response>
        /// <response code="204">No Contact found</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("ActiveContacts")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAllActiveContactsAsync(int pageIndex, int pageSize = 10)
        {
            Log.Information("{Controller} - {Method}", nameof(ContactController), nameof(GetAllActiveContactsAsync));

            if (pageIndex <= 0)
                throw new ValidationException("PageIndex is InValid");
            if (pageSize <= 0)
                throw new ValidationException("PageSize is InValid");

            try
            {
                var contacts = await _contactEngine.GetAllActiveContactsAsync(pageIndex: pageIndex, pageSize: pageSize).ConfigureAwait(false);

                if (contacts.Count() <= 0)
                    return NoContent();

                return Ok(_mapper.Map<IEnumerable<Contact>>(contacts));
            }
            catch (Exception e)
            {
                throw new InternalException("Internal server error", e);
            }
        }

        /// <summary>
        /// Retrieves all in-active contacts
        /// </summary>
        /// <returns>Contacts</returns>
        /// <response code="200">Successfully retrieved in-active Contacts</response>
        /// <response code="204">No Contact found</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("InActiveContacts")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAllInActiveContactsAsync(int pageIndex, int pageSize = 10)
        {
            Log.Information("{Controller} - {Method}", nameof(ContactController), nameof(GetAllActiveContactsAsync));

            if (pageIndex <= 0)
                throw new ValidationException("PageIndex is InValid");
            if (pageSize <= 0)
                throw new ValidationException("PageSize is InValid");

            try
            {
                var contacts = await _contactEngine.GetAllInActiveContactsAsync(pageIndex: pageIndex, pageSize: pageSize).ConfigureAwait(false);

                if (contacts.Count() <= 0)
                    return NoContent();
                
                return Ok(_mapper.Map<IEnumerable<Contact>>(contacts));
            }
            catch (Exception e)
            {
                throw new InternalException("Internal server error", e);
            }
        }

        /// <summary>
        /// Retrieve contact by id
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns>Contacts</returns>
        /// <response code="200">Successfully retrieved in-active Contacts</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("Contact/{contactId}")]
        public async Task<ActionResult<Contact>> GetContactByIdAsync(int contactId)
        {
            Log.Information("{Controller} - {Method}", nameof(ContactController), nameof(GetContactByIdAsync));

            if (contactId <= 0)
                throw new ValidationException("ContactId is InValid");
            try
            {
                Log.Information($"{nameof(ContactController)} - {nameof(GetContactByIdAsync)} - Request- ContactId: {contactId}");
                var contact = await _contactEngine.GetContactByIdAsync(contactId).ConfigureAwait(false);
                return Ok(_mapper.Map<Contact>(contact));
            }
            catch (Exception e)
            {
                Log.Error($"Exception occured for ContactId: {contactId} - Exception - {e.Message}");
                throw new InternalException("Internal server error", e);
            }
        }

        /// <summary>
        /// Adds new contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>Contacts</returns>
        /// <response code="200">Successfully retrieved in-active Contacts</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Route("Contact")]
        [ValidateModel]
        public async Task<ActionResult<bool>> AddContactAsync(Contact contact)
        {
            try
            {
                Log.Information($"{nameof(ContactController)} - {nameof(AddContactAsync)} - Contact: {Newtonsoft.Json.JsonConvert.SerializeObject(contact)}");
                return await _contactEngine.AddContactAsync(_mapper.Map<Domain.Models.Contact>(contact)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Log.Error($"Exception occured while saving contact- Exception: {e.Message}");
                throw new InternalException("Internal server error", e);
            }
        }

        /// <summary>
        /// Updates contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>status</returns>
        /// <response code="200">Successfully updated Contact</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        [Route("Contact")]
        [ValidateModel]
        public async Task<ActionResult<bool>> UpdateContactAsync(Contact contact)
        {
            try
            {
                Log.Information($"{nameof(ContactController)} - {nameof(UpdateContactAsync)} - Contact: {Newtonsoft.Json.JsonConvert.SerializeObject(contact)}");
                return await _contactEngine.UpdateContactAsync(_mapper.Map<Domain.Models.Contact>(contact)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Log.Error($"Exception occured while updating contact- Exception: {e.Message}");
                throw new InternalException("Internal server error", e);
            }
        }

        /// <summary>
        /// Delete contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>status</returns>
        /// <response code="200">Successfully deleted Contact</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete]
        [Route("Contact/{contactId}")]
        public async Task<ActionResult<bool>> DeleteContactAsync(int contactId)
        {
            if (contactId <= 0)
                throw new ValidationException("ContactId is InValid");
            try
            {
                Log.Information($"{nameof(ContactController)} - {nameof(DeleteContactAsync)} - ContactId: {contactId}");
                return await _contactEngine.DeleteContactAsync(contactId).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Log.Error($"Exception occured while deleting contact- Exception: {e.Message}");
                throw new InternalException("Internal server error", e);
            }
        }

        #endregion
    }
}
