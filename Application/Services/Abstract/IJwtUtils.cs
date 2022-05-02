using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Abstract
{
    public interface IJwtUtils
    {
        public Task<string> GenerateJwtToken(User user);

        public Task<int?> ValidateJwtToken(string token);
    }
}
