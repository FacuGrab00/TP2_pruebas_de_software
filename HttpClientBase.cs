using Newtonsoft.Json;
using System.Text;

namespace Trabajo_Practico___Pruebas_de_software
{
    public class HttpClientBase : IDisposable
    {
        protected readonly HttpClient _client;

        public HttpClientBase()
        {
            _client = new HttpClient();
        }

        protected async Task<string> RealizarSolicitud(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);
            
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<string> SolicitudGetConBody(Object value, string url)
        {
            string json = JsonConvert.SerializeObject(value);
            
            StringContent contenido = new StringContent(json, Encoding.UTF8, "application/json");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = contenido
            };

            HttpResponseMessage response = await _client.SendAsync(request);

            return await response.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
