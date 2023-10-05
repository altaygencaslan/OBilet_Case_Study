using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OBilet.Core.DTO.GetBusJourneys
{
    public class BusJourneysRequestDto
    {
        public int OriginId { get; set; }
        public string OriginName { get; set; }

        public int DestinationId { get; set; }
        public string DestinationName { get; set; }

        public DateTime DepartureDate { get; set; } //Dokümanda INT olarak belirtiliyor
    }
}
