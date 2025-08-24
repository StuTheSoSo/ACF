using BackEnd.Models;

namespace BackEnd.Logger
{
    public interface IACFLogger
    {
        Task LogAction(AuditLog log);
    }
}
