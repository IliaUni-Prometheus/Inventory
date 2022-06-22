using Domain.Models.Abstraction;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly NorthwindContext _db;

        // Constructor
        public InvoiceRepository(NorthwindContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Invoice>> RetrieveAllAsync()
        {
            return await _db.Invoices.ToListAsync();
        }
    }
}
