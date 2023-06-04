using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Interfaces
{
    public interface IValidatorInput
    {
        // TODO: check a better way to do this (DRY principle)
        Task<bool> CheckEmailAlreadyExists(string email,CancellationToken cancellation);

        Task<bool> CheckUsernameAlreadyExists(string username, CancellationToken cancellation);
    }
}
