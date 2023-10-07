using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Core.DTO.GetBusJourneys
{
    public class BusJourneysRequestScreenDto
    {
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public string DepartureDate { get; set; }
    }

    public class BusJourneysResponseScreenDto
    {
        public int OriginId { get; set; }
        public string OriginName { get; set; }
        public int DestinationId { get; set; }
        public string DestinationName { get; set; }
        public DateTime DepartureDate { get; set; }
        public BusJourneysResponseDto[] Data { get; set; }
    }

    public class BusJourneysResponseDto
    {
        //Data per boxes
        public int Id { get; set; }

        public int PartnerId { get; set; }
        public string PartnerName { get; set; }

        public int Rank { get; set; }

        public BusJourney Journey { get; set; }
        //Data per boxes
    }

    public class BusJourney
    {
        public string Origin { get; set; }
        public string Destination { get; set; }

        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }

        public decimal OriginalPrice { get; set; }
        public decimal InternetPrice { get; set; }
    }
}
