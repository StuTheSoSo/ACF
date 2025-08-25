using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class User
    {
        /// <summary> Gets or sets the first name. </summary>
        /// <value> The first name. </value>
        public string? FirstName { get; set; } = string.Empty;

        /// <summary> Gets the full name. </summary>
        /// <value> The full name. </value>
        [NotMapped]
        public string FullName
        { get { return $"{FirstName} {LastName}"; } }

        /// <summary> Gets or sets the last name. </summary>
        /// <value> The last name. </value>
        public string? LastName { get; set; } = string.Empty;

        /// <summary> Gets or sets the officer identifier. </summary>
        /// <value> The officer identifier. </value>
        public Guid UserId { get; set; } = Guid.Empty;

        /// <summary> Gets or sets the password hash. </summary>
        /// <value> The password hash. </value>
        public byte[]? PasswordHash { get; set; } = Array.Empty<byte>();

        /// <summary> Gets or sets the password salt. </summary>
        /// <value> The password salt. </value>
        public byte[]? PasswordSalt { get; set; } = Array.Empty<byte>();

        /// <summary> Gets or sets the role identifier. </summary>
        /// <value> The role identifier. </value>
        public Guid RoleId { get; set; } = Guid.Empty;

        /// <summary> Gets or sets the username. </summary>
        /// <value> The username. </value>
        public string Username { get; set; } = string.Empty;
    }
}