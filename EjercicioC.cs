namespace Trabajo_Practico___Pruebas_de_software
{
    public class EjercicioC : HttpClientBase
    {
        private async Task<bool> RealizarSolicitudAsync(string url)
        {
            try
            {
                string responseBody = await base.RealizarSolicitudAsync(url);
                return bool.Parse(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error en la solicitud: {e.Message}");
                return false;
            }
        }


        [Fact]
        async public void EsPasswordSegura_CuandoEsValido_RetornaTrue()
        {
            string password = "Hola2024%";
            string url = $"https://iso-uncaus.somee.com/iso/Test/ValidarPassword/{password}";
            bool resultado = await RealizarSolicitudAsync(url);
            Assert.True(resultado);
        }

        [Theory]
        [InlineData("Contraseña1")]             // Falta un carácter especial
        [InlineData("contraseña1!")]            // Falta una mayúscula
        [InlineData("CONTRASENA!")]             // Falta una minúscula
        [InlineData("Contrasena!")]             // Falta un número
        [InlineData("C1!")]                     // Menos de 8 caracteres
        async public void EsPasswordSegura_CuandoEsInvalido_RetornaFalse(string password)
        {
            string url = $"https://iso-uncaus.somee.com/iso/Test/CalcularDescuento/{password}";
            bool result = await RealizarSolicitudAsync(url);
            Assert.False(result);
        }

    }
}
