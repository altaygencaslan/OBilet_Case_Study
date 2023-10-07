using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBilet.Core.DTO.GetBusLocations
{
    public class BusLocationsResponseDto
    {
        public int Id { get; set; } //Seçim için
        public string LongName { get; set; } //UI için
        public int? Rank { get; set; } //Sıralam için
        public string Keywords { get; set; } //Arama için
        public bool ShowCountry { get; set; }

        public int? CountryId { get; set; }
        public string CountryName { get; set; } //ShowCountry = true ise kullanılacak
    }
}
