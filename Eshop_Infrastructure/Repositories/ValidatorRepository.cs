using Eshop_Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Infrastructure.Repositories
{
    public class ValidatorRepository : IValidatorInput
    {
        private readonly IAppDbContext _context;

        public ValidatorRepository(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckEmailAlreadyExists(string email, CancellationToken cancellation)
        {
            return await _context.User.AnyAsync(x => x.Email == email, cancellation);
        }

        public async Task<bool> CheckUsernameAlreadyExists(string username, CancellationToken cancellation)
        {
            return !await _context.User.AnyAsync(x => x.UserName == username, cancellation);
        }
    }
}
