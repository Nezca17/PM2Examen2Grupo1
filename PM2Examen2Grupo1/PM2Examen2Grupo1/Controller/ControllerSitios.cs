using Newtonsoft.Json;
using PM2Examen2Grupo1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PM2Examen2Grupo1.Controller
{
    public class ControllerSitios
    {
        private static readonly string URL_SITIOS = "https://apex.oracle.com/pls/apex/pm22023/Get_Sitios/todos";
        private static HttpClient client = new HttpClient();

        public static async Task<List<Sitios>> GetAllSite()
        {
            List<Sitios> listBooks = new List<Sitios>();
            try
            {
                var uri = new Uri(URL_SITIOS);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    listBooks = JsonConvert.DeserializeObject<List<Sitios>>(content);
                    return listBooks;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return listBooks;
        }

        public async static Task<bool> DeleteSite(string id)
        {
            try
            {
                var uri = new Uri(URL_SITIOS + "=?idSitios=" + id);
                var result = await client.GetAsync(uri);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async static Task<bool> CreateSite(Sitios sitio)
        {
            try
            {
                Uri requestUri = new Uri(URL_SITIOS);
                var jsonObject = JsonConvert.SerializeObject(sitio);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(requestUri, content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }


        public async static Task<bool> UpdateSitio(Sitios sitio)
        {
            try
            {
                Uri requestUri = new Uri(URL_SITIOS);
                var jsonObject = JsonConvert.SerializeObject(sitio);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                //var response = await client.PutAsync(requestUri, content);
                var response = await client.PostAsync(requestUri, content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
