using Domain.Models.Abstraction;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly NorthwindContext _db;

        // Constructor for injection
        public EmployeeRepository(NorthwindContext db) { _db = db; }

        public async Task<IEnumerable<Employee>> RetrieveAllAsync()
        {
            return await _db.Employees
                    .ToListAsync();
        }
        public async Task<Employee?> RetrieveByIdAsync(int id)
        {
            return await _db.Employees.Where(e => e.EmployeeId == id).FirstOrDefaultAsync();
        }
        public async Task<Employee?> CreateAsync(Employee employee)
        {
            await _db.Employees.AddAsync(employee);
            int affected = await _db.SaveChangesAsync();
            if (affected == 1) { return employee; }

            return null;
        }
        public async Task<bool> UpdateAsync(Employee employee)
        {
            _db.Employees.Update(employee);
            int affected = await _db.SaveChangesAsync();
            return affected == 1;
        }
        public async Task<bool?> DeleteByIdAsync(int id)
        {
            Employee? employee = await _db.Employees.FindAsync(id);
            // Order wasn't found at all
            if (employee == null) { return null; }

            // Deleted successfuly
            _db.Employees.Remove(employee);
            int affected = await _db.SaveChangesAsync();
            if (affected == 1) { return true; }

            // Order was found, but it couldn't be deleted
            return false;
        }
    }
}
