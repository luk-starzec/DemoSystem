using HeaderProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeaderProvider.Services
{
    public interface IHeaderService
    {
        HeaderModel GetHeader();
    }
}
