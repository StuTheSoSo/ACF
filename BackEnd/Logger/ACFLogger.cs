using BackEnd.Data;
using BackEnd.Models;

namespace BackEnd.Logger
{
    public class ACFLogger : IACFLogger
    {
        /// <summary> The context </summary>
        private readonly AppDbContext _context;

        /// <summary> Initializes a new instance of the <see cref="ACFLogger"/> class. </summary>
        /// <param name="context"> The context. </param>
        public ACFLogger(AppDbContext context)
        {
            _context = context;
        }

        /// <summary> Logs the action. </summary>
        /// <param name="log"> The log. </param>
        public async Task LogAction(AuditLog log)
        {
            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}