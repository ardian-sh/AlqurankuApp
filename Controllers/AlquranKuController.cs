using detailsurat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RestSharp;
using selecttwo.Models;
using surat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tafsir.Models;

namespace alquranku.Controllers
{
    public class AlquranKuController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public AlquranKuController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public async Task<IActionResult> Index()
        {
            List<Surat> surats = await GetSurat();

            return View(surats);
        }


        public async Task<List<Surat>> GetSurat()
        {

            var cacheSurat = "getsurat";
            //data pengambilan list surat dismpan di cache, tujuannya adalah ketika user melakukan action
            //tidak perlu mengambil data dari api, cukup mengambil data satu kali saja
            //saat pertama kali browser di muat
            if (!_memoryCache.TryGetValue(cacheSurat, out List<Surat> getSurat))
            {
                //mengambil semua data surat
                var client = new RestClient("https://equran.id/api/surat");
                var request = new RestRequest("", Method.Get);
                RestResponse response = await client.ExecuteAsync(request);

                //konversi json ke list
                getSurat = JsonConvert.DeserializeObject<List<Surat>>(response.Content);          

                //melakukan konfidgurasi untuk cache
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    //cache akan expired setiap 1 hari
                    AbsoluteExpiration = DateTime.Now.AddDays(1),
                    Priority = CacheItemPriority.High,
                    //jika dalam 20 menit user tidak melaukan action,cache akan tereset
                    SlidingExpiration = TimeSpan.FromMinutes(20)
                };
                //set cache
                _memoryCache.Set(cacheSurat, getSurat, cacheExpiryOptions);
            }           

            return getSurat;
        }

        [HttpGet("select-box-surat")]
        public async Task<IActionResult> SelectBoxSurat(string search)
        {
            //mencari semua data surat
            List<Surat> surats = await GetSurat();

            surats = surats.Where(a => a.NamaLatin.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            //data surat di tambahkan ke list select two
            List<SelectTwoModel> results = new();
            foreach (Surat a in surats)
            {
                SelectTwoModel res = new();

                res.Id = a.Nomor;
                res.Text = a.NamaLatin;

                results.Add(res);
            }
            return Json(new { isuccess = true, items = results });
        }

        [HttpGet("search-surat")]
        public async Task<IActionResult> SearchListSurat(string surat)
        {
            List<Surat> surats = await GetSurat();

            surats = surats.Where(a => a.Nomor == surat).ToList();
            if(surats.Count < 1)
            {
                ViewBag.error = "Data tidak ditemukan";
                return View("Index");
            }

            return RedirectToAction("DetailSurat", "AlquranKu", new { surat = surats[0].Nomor });
        }

        [HttpGet("detail-surat")]
        public async Task<IActionResult> DetailSurat(string surat)
        {
            //mengambil semua data ayat
            var client = new RestClient(string.Format("https://equran.id/api/surat/{0}", surat));
            var request = new RestRequest("", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);

            //konversi json ke list
             DetailSuratModel detailSurats = JsonConvert.DeserializeObject<DetailSuratModel>(response.Content);
            if (detailSurats.Status == false)
            {
                ViewBag.error = "Data tidak ditemukan";
                return View("Index");
            }

            //mengambil semua data tafsir
            client = new RestClient(string.Format("https://equran.id/api/tafsir/{0}", surat));
            request = new RestRequest("", Method.Get);
            response =  await client.ExecuteAsync(request);

            //konversi json ke list
            var tafsir = JsonConvert.DeserializeObject<DetailSuratModel>(response.Content).Tafsir;

            detailSurats.Tafsir = tafsir;

            return View(detailSurats);
        }
    }
}
