using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using static Android.Content.ClipData;

namespace PM2Examen2Grupo1.Models
{


    public class Sitios
    {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("latitud")]
        public double Latitud { get; set; }

        [JsonProperty("longitud")]
        public double Longitud { get; set; }

        [JsonProperty("firmadigital")]
        public string Firmadigital { get; set; }

        [JsonProperty("audiofile")]
        public string Audiofile { get; set; }

        public byte[] foto { get; set; }
        public byte[] audio { get; set; }


    }

 

}