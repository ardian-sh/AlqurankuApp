using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace surat.Models
{
    public class Surat
    {
        [JsonProperty("nomor")]
        public string Nomor { get; set; }

        [JsonProperty("nama")]
        public string Nama { get; set; }

        [JsonProperty("nama_latin")]
        public string NamaLatin { get; set; }

        [JsonProperty("jumlah_ayat")]
        public string JumlahAyat { get; set; }

        [JsonProperty("tempat_turun")]
        public string TempatTurun { get; set; }

        [JsonProperty("arti")]
        public string Arti { get; set; }

        [JsonProperty("deskripsi")]
        public string Deskripsi { get; set; }

        [JsonProperty("audio")]
        public string Audio { get; set; }
    }
}
