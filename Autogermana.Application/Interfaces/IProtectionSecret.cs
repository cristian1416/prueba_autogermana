using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autogermana.Application.Interfaces
{
    public interface IProtectionSecret
    {
        string Protect(string secret);
        string Unprotect(string secret);
    }
}
