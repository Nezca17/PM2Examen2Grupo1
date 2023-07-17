using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM2Examen2Grupo1.Models
{
    public class Sitios
    {

        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("descripcion")]
        public string descripcion { get; set; }
        [JsonProperty("latitud")]
        public double Latitud { get; set; }
        [JsonProperty("longitud")]
        public double Longitud { get; set; }
        [JsonProperty("firmadigital")]
        public byte[] FirmaDigital { get; set; }
        [JsonProperty("audioFile")]
        public byte[] audiofile { get; set; }
        [JsonProperty("firma")]
        public string firma { get; set; }

        public byte[] foto { get; set; }

    }
}
