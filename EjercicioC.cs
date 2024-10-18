namespace Trabajo_Practico___Pruebas_de_software
{
    public class EjercicioC : HttpClientBase
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
        [InlineData("a1b2c3d4e5ae")]                      // 12 caracteres (a-z, 0-9)
        [InlineData("aBcDeFgHiJae")]                      // 12 caracteres (a-z, A-Z)
        [InlineData("1234ABCDED56")]                      // 12 caracteres (0-9, A-Z)
        [InlineData("A1BEW2C3D4E5")]                      // 12 caracteres (A-Z, 0-9)
        [InlineData("A!BEB#C$D%EF")]                      // 12 caracteres (A-Z, !@#$%^&)
        [InlineData("a@b#c$d%aee^")]                      // 12 caracteres (a-z, !@#$%^&)
        [InlineData("12!3@4#548$6")]                      // 12 caracteres (0-9, !@#$%^&)
        [InlineData("aB1cD2aeeF3g")]                      // 12 caracteres (a-z, 0-9, A-Z)
        [InlineData("a1!b2ae@c3#d4")]                     // 12 caracteres (a-z, 0-9, !@#$%^&)
        [InlineData("aB1!cD2@eFae")]                      // 12 caracteres (a-z, 0-9, A-Z, !@#$%^&)
        async public void PasswordSegura(string password)
        {
            string url = $"https://iso-uncaus.somee.com/iso/Test/CalcularDescuento/{password}";
            bool result = await Solicitud(url);
            Assert.True(result);
        }

        [Theory]
        [InlineData("")]                                // Cadena vacia
        [InlineData("a")]                               // Un solo carácter (a-z)
        [InlineData("1")]                               // Un solo carácter (0-9)
        [InlineData("A")]                               // Un solo carácter (A-Z)
        [InlineData("#")]                               // Un solo carácter (!@#$%^&)
        [InlineData("ab")]                              // Dos caracteres (a-z)
        [InlineData("1b")]                              // Dos caracteres (0-9, a-z)
        [InlineData("Ab")]                              // Dos caracteres (A-Z, a-z)
        [InlineData("@b")]                              // Dos caracteres (!@#$%^&, a-z)
        [InlineData("12")]                              // Dos caracteres (0-9)
        [InlineData("1A")]                              // Dos caracteres (0-9, A-Z)
        [InlineData("1#")]                              // Dos caracteres (0-9, !@#$%^&)
        [InlineData("AB")]                              // Dos caracteres (A-Z)
        [InlineData("A#")]                              // Dos caracteres (A-Z, !@#$%^&)
        [InlineData("$@")]                              // Dos caracteres (!@#$%^&)
        [InlineData("abc")]                             // Tres caracteres (a-z)
        [InlineData("1bc")]                             // Tres caracteres (0-9, a-z)
        [InlineData("Abc")]                             // Tres caracteres (A-Z, a-z)
        [InlineData("@bc")]                             // Tres caracteres (!@#$%^&, a-z)
        [InlineData("12b")]                             // Tres caracteres (0-9, a-z)
        [InlineData("1AB")]                             // Tres caracteres (0-9, A-Z)
        [InlineData("1#b")]                             // Tres caracteres (0-9, a-z, !@#$%^&)
        [InlineData("ABc")]                             // Tres caracteres (A-Z, a-z)
        [InlineData("A#b")]                             // Tres caracteres (A-Z, a-z, !@#$%^&)
        [InlineData("$@c")]                             // Tres caracteres (!@#$%^&, a-z)
        [InlineData("123")]                             // Tres caracteres (0-9)
        [InlineData("1A@")]                             // Tres caracteres (0-9, A-Z, !@#$%^&)
        [InlineData("AB#")]                             // Tres caracteres (A-Z, !@#$%^&)
        [InlineData("$@1")]                             // Tres caracteres (!@#$%^&, 0-9)
        [InlineData("XYZ")]                             // Tres caracteres (A-Z)
        [InlineData("!*@")]                             // Tres caracteres (!@#$%^&)
        [InlineData("fxtrhcv")]                         // Menos de 8 caracteres (a-z)
        [InlineData("0kueza9")]                         // Menos de 8 caracteres (a-z) (0-9)
        [InlineData("QdkrBLa")]                         // Menos de 8 caracteres (a-z) (A-Z)
        [InlineData("CHNR&^s")]                         // Menos de 8 caracteres (a-z) (!@#$%^&)
        [InlineData("Ux2mbYa")]                         // Menos de 8 caracteres (a-z) (0-9) (A-Z)
        [InlineData("MR&409p")]                         // Menos de 8 caracteres (a-z) (0-9) (!@#$%^&)
        [InlineData("RPsK&Ph")]                         // Menos de 8 caracteres (a-z) (A-Z) (!@#$%^&)
        [InlineData("m#W1*r7")]                         // Menos de 8 caracteres (a-z) (A-Z) (0-9) (!@#$%^&)
        [InlineData("1234567")]                         // Menos de 8 caracteres (0-9)
        [InlineData("9P4J72K")]                         // Menos de 8 caracteres (0-9) (A-Z)
        [InlineData("8&$3^59")]                         // Menos de 8 caracteres (0-9) (!@#$%^&)
        [InlineData("5A#7B9T")]                         // Menos de 8 caracteres (0-9) (A-Z) (!@#$%^&)
        [InlineData("BCDFGHJ")]                         // Menos de 8 caracteres (A-Z)
        [InlineData("H&JK#LZ")]                         // Menos de 8 caracteres (A-Z) (!@#$%^&)
        [InlineData("@^$*#&!")]                         // Menos de 8 caracteres (!@#$%^&)
        async public void PasswordInsegura(string password)
        {
            string url = $"https://iso-uncaus.somee.com/iso/Test/CalcularDescuento/{password}";
            bool result = await Solicitud(url);
            Assert.False(result);
        }

    }
}
