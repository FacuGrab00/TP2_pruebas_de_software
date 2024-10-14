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
        [InlineData(3, 3, 3, "Equilátero")]         // Todos los lados iguales
        [InlineData(3, 4, 4, "Isósceles")]          // Dos lados iguales
        [InlineData(3, 4, 5, "Escaleno")]           // Todos los lados diferentes
        [InlineData(1, 10, 12, "Escaleno")]
        [InlineData(10, 1, 1, "Isoceles")]
        [InlineData(2147483647, 2147483647, 2147483647, "Equilátero")]
        async public void TipoDeTriangulo_ValoresValidos_TipoCorrecto(int lado1, int lado2, int lado3, string tipoEsperado)
        {
            string url = $"https://iso-uncaus.somee.com/iso/Test/TipoTriangulo/{lado1}/{lado2}/{lado3}";
            string resultado = await Solicitud(url);
            Assert.Equal(tipoEsperado, resultado);
        }

        [Theory]
        [InlineData(0, 0, 0)]               // Lado cero no válido
        [InlineData(0, 3, 3)]               // Lado cero no válido
        [InlineData(3, 0, 3)]               // Lado cero no válido
        [InlineData(3, 3, 0)]               // Lado cero no válido
        [InlineData(0, 0, 3)]               // Lado cero no válido
        [InlineData(3, 0, 0)]               // Lado cero no válido
        [InlineData(0, 3, 0)]               // Lado cero no válido
        [InlineData(-1, 4, 5)]              // Lado negativo no válido
        [InlineData(1, -4, 5)]              // Lado negativo no válido
        [InlineData(1, 4, -5)]              // Lado negativo no válido
        [InlineData(-1, -4, 5)]             // Lado negativo no válido
        [InlineData(-1, 4, -5)]             // Lado negativo no válido
        [InlineData(1, -4, -5)]             // Lado negativo no válido
        [InlineData(-1, -4, -5)]            // Lado negativo no válido
        [InlineData(10000, 10000, 1)]       // No forma un triangulo por desigualdad triangular
        [InlineData(0, -3, 3)]              // Lado cero, lado negativo no válido
        async public void TipoDeTriangulo_ValoresInvalidos_NoEsTrianguloValido(int lado1, int lado2, int lado3)
        {
            string tipoEsperado = "No válido";
            string url = $"https://iso-uncaus.somee.com/iso/Test/TipoTriangulo/{lado1}/{lado2}/{lado3}";
            string resultado = await Solicitud(url);
            Assert.Equal(tipoEsperado, resultado);
        }
    }
}
