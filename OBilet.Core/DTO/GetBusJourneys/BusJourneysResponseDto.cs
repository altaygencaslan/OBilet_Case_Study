using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Core.DTO.GetBusJourneys
{
    public class BusJourneysResponseDto : BusJourneysResponse<BusJourneysData>
    {

    }

    public class BusJourneysResponse<T>
    {
        //Header
        public string OriginName { get; set; }
        public string DestinationName { get; set; }
        public DateTime DepartureDate { get; set; }
        //Header

        public T[] Data { get; set; }
    }

    public class BusJourneysData
    {
        //Data per boxes
        public int Id { get; set; }

        public int PartnerId { get; set; }
        public string PartnerName { get; set; }

        public string Origin { get; set; }
        public string Destination { get; set; }

        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }

        public decimal OriginalPrice { get; set; }
        public decimal InternetPrice { get; set; }
        //Data per boxes
    }
}
