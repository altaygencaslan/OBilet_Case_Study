using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Integration.Services.Model.Base
{
    public class GeneralResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }

        public GeneralResponse(T data)
        {
            IsSuccess = true;
            Data = data;
        }

        public GeneralResponse()
        {
            IsSuccess = false;
        }
    }
}
