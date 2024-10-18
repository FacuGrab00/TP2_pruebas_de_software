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
        [InlineData(18)]
        [InlineData(20)]
        [InlineData(80)]
        [InlineData(120)]
        public async Task AptoParaVotar(int edad)
        {
            // Preparación
            string url = $"https://iso-uncaus.somee.com/iso/Test/HabilitadoParaVotar/{edad}";

            // Acción
            bool resultado = await Solicitud(url);

            // Assert
            Assert.True(resultado);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(17)]
        public async Task NoAptoParaVotar(int edad)
        {
            // Preparación
            string url = $"https://iso-uncaus.somee.com/iso/Test/HabilitadoParaVotar/{edad}";

            // Acción
            bool resultado = await Solicitud(url);

            // Assert
            Assert.False(resultado);
        }


        [Theory]
        [InlineData(1000)]
        [InlineData(150)]
        [InlineData(-1)]
        public async Task NoValidosParaVotar(int edad)
        {
            // Preparación
            string url = $"https://iso-uncaus.somee.com/iso/Test/HabilitadoParaVotar/{edad}";

            // Acción
            bool resultado = await Solicitud(url);

            // Assert
            Assert.False(resultado);
        }
    }
}