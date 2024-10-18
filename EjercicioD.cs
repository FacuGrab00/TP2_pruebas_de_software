namespace Trabajo_Practico___Pruebas_de_software
{
    public class EjercicioD : HttpClientBase
    {
        private async Task<string> Solicitud(string url)
        {
            try
            {
                return await base.RealizarSolicitud(url);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error en la solicitud: {e.Message}");
                return "Error";
            }
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(3, 3, 3)]
        [InlineData(10, 10, 10)]
        async public void TrianguloEquilatero(int lado1, int lado2, int lado3)
        {
            string tipoEsperado = "Equilátero";
            string url = $"https://iso-uncaus.somee.com/iso/Test/TipoTriangulo/{lado1}/{lado2}/{lado3}";
            string resultado = await Solicitud(url);
            Assert.Equal(tipoEsperado, resultado);
        }
        
        [Theory]
        [InlineData(4, 4, 3)]
        [InlineData(4, 3, 4)]
        [InlineData(3, 4, 4)]
        async public void TriangulosIsoceles(int lado1, int lado2, int lado3)
        {
            string tipoEsperado = "Isósceles";
            string url = $"https://iso-uncaus.somee.com/iso/Test/TipoTriangulo/{lado1}/{lado2}/{lado3}";
            string resultado = await Solicitud(url);
            Assert.Equal(tipoEsperado, resultado);
        }

        [InlineData(3, 4, 5)]
        [InlineData(3, 5, 4)]
        [InlineData(4, 3, 5)]
        [InlineData(4, 5, 3)]
        [InlineData(5, 3, 4)]
        [InlineData(5, 4, 3)]
        async public void TrianguloEscaleno(int lado1, int lado2, int lado3)
        {
            string tipoEsperado = "Escaleno";
            string url = $"https://iso-uncaus.somee.com/iso/Test/TipoTriangulo/{lado1}/{lado2}/{lado3}";
            string resultado = await Solicitud(url);
            Assert.Equal(tipoEsperado, resultado);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 3, 3)]
        [InlineData(3, 0, 3)]
        [InlineData(3, 3, 0)]
        [InlineData(0, 0, 3)]
        [InlineData(3, 0, 0)]
        [InlineData(0, 3, 0)]
        [InlineData(-1, 4, 5)]
        [InlineData(1, -4, 5)]
        [InlineData(1, 4, -5)]
        [InlineData(-1, -4, 5)]
        [InlineData(-1, 4, -5)]
        [InlineData(1, -4, -5)]
        [InlineData(-1, -4, -5)]
        [InlineData(0, -3, 3)]
        [InlineData(0, 0, -1)]
        [InlineData(0, -1, 0)]
        [InlineData(-1, 0, 0)]
        [InlineData(0, -1, -1)]
        [InlineData(-1, 0, -1)]
        [InlineData(-1, -1, 0)]
        [InlineData(-1, -1, -1)]
        [InlineData(10000, 10000, 1)]
        async public void TrianguloNoValidos(int lado1, int lado2, int lado3)
        {
            string tipoEsperado = "No válido";
            string url = $"https://iso-uncaus.somee.com/iso/Test/TipoTriangulo/{lado1}/{lado2}/{lado3}";
            string resultado = await Solicitud(url);
            Assert.Equal(tipoEsperado, resultado);
        }
    }
}
