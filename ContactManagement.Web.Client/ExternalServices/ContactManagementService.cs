namespace ContactManagement.Web.Client.ExternalServices
{
    using ContactManagement.Contracts.Models;
    using ContactManagement.Web.Client.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Serilog;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    public class ContactManagementService : IContactManagementService
    {
        #region Fields

        private const string ServicesKey = "Services";
        private const string ContactManagementWebApiURLKey = "ContactManagementWebApiURL";
        private readonly string _contactManagementWebApiURL;
        private readonly IHttpClientFactory _httpClientFactory;

        #endregion

        #region Constructor

        public ContactManagementService(IConfiguration configuration, IHttpClientFactory httpClientFactor)
        {
            _httpClientFactory = httpClientFactor ?? throw new ArgumentNullException(nameof(httpClientFactor));
            _contactManagementWebApiURL = configuration[$"{ServicesKey}:{ContactManagementWebApiURLKey}"];
            if (_contactManagementWebApiURL.EndsWith('/'))
                _contactManagementWebApiURL = _contactManagementWebApiURL.Trim('/');
        }

        #endregion

        #region Public Methods

        public async Task<HttpResponseMessage> GetAllContactsAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var url = $"{_contactManagementWebApiURL}/api/Contacts";

            return await client.GetAsync(new Uri(url)).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> GetAllActiveContactsAsync(int pageIndex, int pageSize = 10)
        {
            Log.Information("{Service} - {Method} - Retrieving all active contacts.", nameof(ContactManagementService), nameof(GetAllActiveContactsAsync));

            return await GetContacts("Active", pageIndex, pageSize).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> GetAllInActiveContactsAsync(int pageIndex, int pageSize = 10)
        {
            Log.Information("{Service} - {Method} - Retrieving all in-active contacts.", nameof(ContactManagementService), nameof(GetAllInActiveContactsAsync));

            return await GetContacts("InActive", pageIndex, pageSize).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> GetContactByIdAsync(int id)
        {
            Log.Information("{Service} - {Method} - Retrieving contact by Id : {id}.", nameof(ContactManagementService), nameof(GetContactByIdAsync),id);

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var url = $"{_contactManagementWebApiURL}/api/Contacts/{id}";

            return await client.GetAsync(new Uri(url)).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> AddContactAsync(Contact contact)
        {
            Log.Information("{Service} - {Method} - Adding new Contact.", nameof(ContactManagementService), nameof(AddContactAsync));

            var client = _httpClientFactory.CreateClient();
            var url = $"{_contactManagementWebApiURL}/api/Contacts";

            var content = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");

            return await client.PostAsync(new Uri(url), content)
                .ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> DeleteContactAsync(int id)
        {
            Log.Information("{Service} - {Method} - Deleting contact by Id : {id}.", nameof(ContactManagementService), nameof(DeleteContactAsync), id);

            var client = _httpClientFactory.CreateClient();
            var url = $"{_contactManagementWebApiURL}/api/Contacts/{id}";

            return await client.DeleteAsync(new Uri(url)).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> UpdateContactAsync(Contact contact)
        {
            Log.Information("{Service} - {Method} - updating contact by Id : {id}.", nameof(ContactManagementService), nameof(UpdateContactAsync), contact.ContactId);

            var client = _httpClientFactory.CreateClient();
            var url = $"{_contactManagementWebApiURL}/api/Contacts";

            var content = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");

            return await client.PutAsync(new Uri(url), content)
                .ConfigureAwait(false);
        }

        #endregion

        #region Private Methods

        private async Task<HttpResponseMessage> GetContacts(string contactStatus, int pageIndex, int pageSize)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var url = $"{_contactManagementWebApiURL}/api/Contacts/{contactStatus}?pageIndex={pageIndex}&pageSize={pageSize}";

            return await client.GetAsync(new Uri(url)).ConfigureAwait(false);
        }

        #endregion
    }
}
