using Brights.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Brights.BLL.Abstract
{
    public interface IServiceHttpRequest
    {
        Task<ResponseModel> RequestToUrl(string url);
    }
}
