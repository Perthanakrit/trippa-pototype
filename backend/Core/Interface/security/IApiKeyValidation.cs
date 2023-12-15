using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interface.security
{
    public interface IApiKeyValidation
    {
        bool Validate(string apiKey);
    }
}