using Autogermana.Application.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Autogermana.Infrastructure.Security
{
    public sealed class ProtectionSecret : IProtectionSecret
    {
        private readonly IDataProtector _dataProtector;

        public ProtectionSecret(IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector("abc");
        }

        public string Protect(string secret)
        {
            return _dataProtector.Protect(secret);
        }

        public string Unprotect(string secret)
        {
            return _dataProtector.Unprotect(secret);
        }
    }
}
