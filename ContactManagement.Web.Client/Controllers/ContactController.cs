using ContactManagement.Contracts.Models;
using ContactManagement.Web.Client.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;

namespace ContactManagement.Web.Client.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactManagementService _contactManagementService;
        public ContactController(IContactManagementService contactManagementService)
        {
            _contactManagementService = contactManagementService ?? throw new ArgumentNullException(nameof(contactManagementService));
        }

        public async Task<IActionResult> Index()
        {
            Log.Information("{Controller} - {Action}", nameof(ContactController), nameof(Index));

            var response = await _contactManagementService.GetAllContactsAsync().ConfigureAwait(false);

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<IEnumerable<Contact>>(responseJson);

            return View(result);
        }
        public async Task<IActionResult> Active()
        {
            Log.Information("{Controller} - {Action}", nameof(ContactController), nameof(Active));

            var response = await _contactManagementService.GetAllActiveContactsAsync(1, int.MaxValue).ConfigureAwait(false);

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<IEnumerable<Contact>>(responseJson);

            return View("Index", result);
        }
        public async Task<IActionResult> InActive()
        {
            Log.Information("{Controller} - {Action}", nameof(ContactController), nameof(InActive));

            var response = await _contactManagementService.GetAllInActiveContactsAsync(1, int.MaxValue).ConfigureAwait(false);

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<IEnumerable<Contact>>(responseJson);

            return View("Index", result);
        }

        public IActionResult CreateContact()
        {
            Log.Information("{Controller} - {Action}", nameof(ContactController), nameof(CreateContact));

            return View();
        }

        public async Task<IActionResult> EditContact(int id)
        {
            Log.Information("{Controller} - {Action}", nameof(ContactController), nameof(EditContact));

            var response = await _contactManagementService.GetContactByIdAsync(id).ConfigureAwait(false);
            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<Contact>(responseJson);
            return View(result);
        }

        public async Task<IActionResult> ContactDetails(int id)
        {
            Log.Information("{Controller} - {Action}", nameof(ContactController), nameof(ContactDetails));

            var response = await _contactManagementService.GetContactByIdAsync(id).ConfigureAwait(false);
            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<Contact>(responseJson);
            return View(result);
        }

        public async Task<IActionResult> DeleteContact(int id)
        {
            Log.Information("{Controller} - {Action}", nameof(ContactController), nameof(DeleteContact));

            var response = await _contactManagementService.GetContactByIdAsync(id).ConfigureAwait(false);
            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<Contact>(responseJson);
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateContact(Contact contact)
        {
            Log.Information("{Controller} - {Request}", nameof(ContactController), nameof(CreateContact));

            var result = await _contactManagementService.AddContactAsync(contact);
            if (result.IsSuccessStatusCode && JsonConvert.DeserializeObject<bool>(result.Content.ReadAsStringAsync().Result) == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to add contact");
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditContact(Contact contact)
        {
            Log.Information("{Controller} - {Request}", nameof(ContactController), nameof(EditContact));

            var result = await _contactManagementService.UpdateContactAsync(contact);
            if (result.IsSuccessStatusCode && JsonConvert.DeserializeObject<bool>(result.Content.ReadAsStringAsync().Result) == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to update contact");
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteContactById(int id)
        {
            Log.Information("{Controller} - {Request}", nameof(ContactController), nameof(DeleteContactById));

            var result = await _contactManagementService.DeleteContactAsync(id);
            if (result.IsSuccessStatusCode && JsonConvert.DeserializeObject<bool>(result.Content.ReadAsStringAsync().Result) == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to delete contact");
                return BadRequest();
            }
        }
    }
}