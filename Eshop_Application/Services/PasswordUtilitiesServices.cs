using Eshop_Application.Common.Exceptions;
using Eshop_Application.Common.Interfaces;
using Eshop_Application.Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Services
{
    public class PasswordUtilitiesServices : IPasswordUtilities
    {
        //Using the IOptions Pattern
        private readonly HashSettings _hashConfig;

        public PasswordUtilitiesServices(IOptions<HashSettings> hashConfig)
        {
            _hashConfig = hashConfig.Value;
        }
        public string GenerateUserHashAndSalt(string password, out byte[] salt)
        {
            // Generating a salt base in a count
            salt = RandomNumberGenerator.GetBytes(_hashConfig.KeySize);


            //Generating a hash base in differents params
            var userHash = Rfc2898DeriveBytes
                            .Pbkdf2(password,
                                        salt,
                                        _hashConfig.Iterations,
                                        HashAlgorithmName.SHA512,
                                        _hashConfig.KeySize);


            //Converting from byte[] to string
            return Convert.ToHexString(userHash);
        }

        public void verifyPasswordsEquality(string password, string confirmPassword)
        {
            if (!password.Equals(confirmPassword))
            {
                throw new PasswordException("The password and confirm password field have to be the same");
            }

            return;
        }

        public bool VerifyPasswordHash(string password, string hash, byte[] salt)
        {
            //Recreating the hash with the password gave
            var hashCompare = Rfc2898DeriveBytes
                              .Pbkdf2(password,
                                          salt,
                                          _hashConfig.Iterations,
                                          HashAlgorithmName.SHA512,
                                          _hashConfig.KeySize);

            //Comparing the hash recreated with the hash saved in the DB
            return hashCompare.SequenceEqual(Convert.FromHexString(hash));
        }



    }
}
