namespace BackEnd.Models
{
    public class LoginObject
    {
        /// <summary> Initializes a new instance of the <see cref="LoginObject"/> class. </summary>
        /// <param name="username"> The username. </param>
        /// <param name="password"> The password. </param>
        public LoginObject(string username, string password)
        {
            Username = username;
            Password = password;
        }

        /// <summary> Initializes a new instance of the <see cref="LoginObject"/> class. </summary>
        public LoginObject()
        { }

        /// <summary> Gets or sets the password. </summary>
        /// <value> The password. </value>
        public string Password { get; set; } = string.Empty;

        /// <summary> Gets or sets the username. </summary>
        /// <value> The username. </value>
        public string Username { get; set; } = string.Empty;

        // Parameterless constructor for deserialization
    }
}