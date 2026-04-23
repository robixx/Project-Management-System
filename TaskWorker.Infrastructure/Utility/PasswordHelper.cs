using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorker.Infrastructure.Utility
{
    public static class PasswordHelper
    {
        private static readonly PasswordHasher<object> _hasher = new();

        public static string Hash(string password)
        {
            return _hasher.HashPassword("", password);
        }

        public static bool Verify(string hashedPassword, string password)
        {
            return _hasher.VerifyHashedPassword("", hashedPassword, password)
                   == PasswordVerificationResult.Success;
        }
    }
}
