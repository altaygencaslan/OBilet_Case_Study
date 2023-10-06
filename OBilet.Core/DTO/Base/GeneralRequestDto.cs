using OBilet.Core.DTO.GetSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Core.DTO.Base
{
    public class GeneralRequestDto<T>
    {
        public SessionRequestDto SessionRequest { get; set; }
        public T RequestItem { get; set; }
    }
}
