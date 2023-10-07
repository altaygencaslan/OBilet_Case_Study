using Microsoft.AspNetCore.Mvc.Rendering;
using OBilet.Core.DTO.GetBusLocations;
using System.Text.Json.Serialization;

namespace OBilet.Web.Models
{
    public class BusLocationSearchModel
    {
        public int OriginId { get; set; }

        public int DestionationId { get; set; }
        public string DepartureDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");

        public SelectListItem[] OriginLocations { get; set; }
        public SelectListItem[] DestinationLocations { get; set; }

    }
}
