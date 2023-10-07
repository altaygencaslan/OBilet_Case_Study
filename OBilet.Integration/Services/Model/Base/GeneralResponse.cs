using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Integration.Services.Model.Base
{
    public class GeneralResponse<T>
    {
        private bool _success;
        public bool IsSuccess
        {
            get
            {
                if (!_success)
                {
                    _success = Data != null;
                }

                return _success;
            }
            set
            {
                _success = value;
            }
        }

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
