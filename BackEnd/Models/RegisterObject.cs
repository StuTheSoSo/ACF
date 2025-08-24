namespace BackEnd.Models
{
    public class RegisterObject
    {
        /// <summary> Gets or sets the first name. </summary>
        /// <value> The first name. </value>
        public string FirstName { get; set; } = string.Empty;

        /// <summary> Gets or sets the last name. </summary>
        /// <value> The last name. </value>
        public string LastName { get; set; } = string.Empty;

        /// <summary> Gets or sets the password. </summary>
        /// <value> The password. </value>
        public string Password { get; set; } = string.Empty;

        /// <summary> Gets or sets the role. </summary>
        /// <value> The role. </value>
        public Guid Role { get; set; } = Guid.Empty;

        /// <summary> Gets or sets the name of the user. </summary>
        /// <value> The name of the user. </value>
        public string UserName { get; set; } = string.Empty;
    }
}