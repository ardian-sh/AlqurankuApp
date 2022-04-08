using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace detailsurat.Models
{
    public class DetailSuratModel
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

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

        [JsonProperty("ayat")]
        public List<AyatList> Ayat { get; set; }

        [JsonProperty("tafsir")]
        public List<TafsirList> Tafsir { get; set; }


    }

    public class AyatList
    {
        [JsonProperty("nomor")]
        public string Nomor { get; set; }

        [JsonProperty("ar")]
        public string Ar { get; set; }

        [JsonProperty("tr")]
        public string Tr { get; set; }

        [JsonProperty("idn")]
        public string Idn { get; set; }
    }

    public class TafsirList
    {
        [JsonProperty("ayat")]
        public string Ayat { get; set; }

        [JsonProperty("tafsir")]
        public string Tafsir { get; set; }
    }
}
