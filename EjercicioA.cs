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
        [InlineData(18, true)]          // Prueba frontera
        [InlineData(17, false)]         // Prueba frontera
        [InlineData(20, true)]          // Caso positivo
        [InlineData(80, true)]          // Caso positivo
        [InlineData(120, true)]         // Caso positivo
        [InlineData(100000, true)]      // Caso positivo
        [InlineData(1, false)]          // Caso negativo
        [InlineData(-1, false)]         // Caso negativo
        [InlineData(0, false)]          // Caso negativo
        public async Task HabilitadoParaVotar(int edad, bool esperado)
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