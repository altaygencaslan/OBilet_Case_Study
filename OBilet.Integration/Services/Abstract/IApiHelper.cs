using OBilet.Integration.Services.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Integration.Services.Abstract
{
    public interface IApiHelper
    {
        Task<GeneralResponse<T>> PostAsync<T, K>(string baseUrl, string endpointUrl, K data);
    }
}
