namespace ContactManagement.Web.Client.ExternalServices
{
    using ContactManagement.Contracts.Models;
    using ContactManagement.Web.Client.Interfaces;
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    public class ContactManagementService : IContactManagementService
    {
        #region Fields

        private const string CONTACT_BASE_URI = "https://localhost/ContactManagement.Web.Api";
        private readonly IContactManagementService _contactManagementService;
        private readonly IHttpClientFactory _httpClientFactory;
        #endregion
        #region Constructor

        public ContactManagementService(IHttpClientFactory httpClientFactor)
        {
            _httpClientFactory = httpClientFactor ?? throw new ArgumentNullException(nameof(httpClientFactor));
        }
        #endregion

        #region Public Methods
        public async Task<HttpResponseMessage> GetAllActiveContactsAsync(int pageIndex, int pageSize = 10)
        {
            return await GetContacts("ActiveContacts", pageIndex, pageSize).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> GetAllInActiveContactsAsync(int pageIndex, int pageSize = 10)
        {
            return await GetContacts("InActiveContacts", pageIndex, pageSize).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> GetContactByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var url = $"{CONTACT_BASE_URI}/api/Contact/Contact/{id}";

            return await client.GetAsync(new Uri(url)).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> AddContactAsync(Contact contact)
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"{CONTACT_BASE_URI}/api/Contact/Contact";

            var content = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");

            return await client.PostAsync(new Uri(url), content)
                .ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> DeleteContactAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"{CONTACT_BASE_URI}/api/Contact/Contact/{id}";

            return await client.DeleteAsync(new Uri(url)).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> UpdateContactAsync(Contact contact)
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"{CONTACT_BASE_URI}/api/Contact/Contact";

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

            var url = $"{CONTACT_BASE_URI}/api/Contact/{contactStatus}?pageIndex={pageIndex}&pageSize={pageSize}";

            return await client.GetAsync(new Uri(url)).ConfigureAwait(false);
        }
        #endregion
    }
}
