using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Integration.Services.Model.Const
{
    public class ResponseStatus
    {
        public const string Success ="Success";

        public const string InvalidDepartureDate = "InvalidDepartureDate";

        public const string InvalidRoute = "InvalidRoute";

        public const string InvalidLocation = "InvalidLocation";

        public const string Timeout = "Timeout";
    }
}
