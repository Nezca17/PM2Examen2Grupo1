using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM2Examen2Grupo1.Models
{
    public class Sitios
    {

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }
        [JsonProperty("latitud")]
        public string Latitud { get; set; }
        [JsonProperty("longitud")]
        public string Longitud { get; set; }
        [JsonProperty("firma2")]
        public byte[] FirmaDigital { get; set; }
        [JsonProperty("audioFile")]
        public byte[] AudioFile { get; set; }
        [JsonProperty("firma")]
        public string firma { get; set; }

        public byte[] foto { get; set; }

    }
}
