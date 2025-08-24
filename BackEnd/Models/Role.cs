namespace BackEnd.Models
{
    public class Role
    {
        /// <summary> Gets or sets the role identifier. </summary>
        /// <value> The role identifier. </value>
        public Guid RoleId { get; set; } = Guid.Empty;

        /// <summary> Gets or sets the name of the role. </summary>
        /// <value> The name of the role. </value>
        public string? RoleName { get; set; } = string.Empty;
    }
}