namespace ContactManagement.Contracts.Models
{
    using System.ComponentModel.DataAnnotations;

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
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Email Address
        /// </summary>
        [Required]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Please enter valid email.")]
        public string Email { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        [Required]
        [StringLength(50)]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone number.")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Active status
        /// </summary>
        public bool IsActive { get; set; }
    }

}
