namespace BackEnd.Models
{
    public class Role
    {
        public Guid RoleId { get; set; } = Guid.Empty;
        public string? RoleName { get; set; } = string.Empty;
    }
}
