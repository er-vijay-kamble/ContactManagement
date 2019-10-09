namespace ContactManagement.Domain.Models
{
    /// <summary>
    /// Contact
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Contact Id
        /// </summary>
        public int ContactId { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email Address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Active status
        /// </summary>
        public bool IsActive { get; set; }
    }

}
