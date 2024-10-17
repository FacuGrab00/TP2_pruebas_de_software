namespace Trabajo_Practico___Pruebas_de_software
{
    public class EjercicioA : HttpClientBase
    {
        private async Task<bool> Solicitud(string url)
        {
            try
            {
                string responseBody = await base.RealizarSolicitud(url);
                return bool.Parse(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error en la solicitud: {e.Message}");
                return false;
            }
        }

        [Theory]
        [InlineData(18, true)]
        [InlineData(17, false)]
        [InlineData(20, true)]
        [InlineData(80, true)]
        [InlineData(120, true)]
        public async Task AptoParaVotar(int edad, bool esperado)
        {
            // Preparación
            string url = $"https://iso-uncaus.somee.com/iso/Test/HabilitadoParaVotar/{edad}";

            // Acción
            bool resultado = await Solicitud(url);

            // Assert
            Assert.Equal(esperado, resultado);
        }

        [Theory]
        [InlineData(100000, false)]
        [InlineData(1, false)]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        public async Task NoAptoParaVotar(int edad, bool esperado)
        {
            // Preparación
            string url = $"https://iso-uncaus.somee.com/iso/Test/HabilitadoParaVotar/{edad}";

            // Acción
            bool resultado = await Solicitud(url);

            // Assert
            Assert.Equal(esperado, resultado);
        }
    }
}