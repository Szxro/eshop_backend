using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Interfaces;

public interface IPasswordUtilities
{
    string GenerateUserHashAndSalt(string password, out byte[] salt);

    bool VerifyPasswordHash(string password, string hash, byte[] salt);

    void verifyPasswordsEquality(string password,string confirmPassword);
}
