using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Client
    {
        /// <summary> Gets or sets the client identifier. </summary>
        /// <value> The client identifier. </value>
        public Guid? ClientId { get; set; } = Guid.Empty;

        /// <summary> Gets or sets the eligibility. </summary>
        /// <value> The eligibility. </value>
        public string Eligibility { get; set; } = string.Empty;

        /// <summary> Gets or sets the first name. </summary>
        /// <value> The first name. </value>
        public string FirstName { get; set; } = string.Empty;

        /// <summary> Gets the full name. </summary>
        /// <value> The full name. </value>
        [NotMapped]
        public string FullName
        { get { return $"{FirstName} {LastName}"; } }

        /// <summary> Gets or sets the last name. </summary>
        /// <value> The last name. </value>
        public string LastName { get; set; } = string.Empty;

        /// <summary> Gets or sets the SSN. </summary>
        /// <value> The SSN. </value>
        public string SSN { get; set; } = string.Empty;
    }
}