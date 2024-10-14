namespace Trabajo_Practico___Pruebas_de_software
{
    public class HttpClientBase : IDisposable
    {
        protected readonly HttpClient _client;

        public HttpClientBase()
        {
            _client = new HttpClient();
        }

        protected async Task<string> RealizarSolicitudAsync(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
