namespace Trabajo_Practico___Pruebas_de_software
{
    public class EjercicioB : HttpClientBase
    {
        private async Task<double> RealizarSolicitudAsync(string url)
        {
            string responseBody = await base.RealizarSolicitudAsync(url);
            return double.Parse(responseBody);
        }

        [Fact]
        public async Task CalcularDescuento_MontoMenor10000_Devuelve10Porciento()
        {
            double montoCompra = 9000;
            string url = $"https://iso-uncaus.somee.com/iso/Test/CalcularDescuento/{montoCompra}";
            double result = await RealizarSolicitudAsync(url);
            Assert.Equal(900, result); // 10% de 9000 es 900
        }

        [Fact]
        public async Task CalcularDescuento_MontoEntre10000y19999_Devuelve15Porciento()
        {
            double montoCompra = 15000;
            string url = $"https://iso-uncaus.somee.com/iso/Test/CalcularDescuento/{montoCompra}";
            double result = await RealizarSolicitudAsync(url);
            Assert.Equal(2250, result); // 15% de 15000 es 2250
        }

        [Fact]
        public async Task CalcularDescuento_MontoMayorOIgual20000_Devuelve20Porciento()
        {
            double montoCompra = 20000;
            string url = $"https://iso-uncaus.somee.com/iso/Test/CalcularDescuento/{montoCompra}";
            double result = await RealizarSolicitudAsync(url);
            Assert.Equal(4000, result); // 20% de 20000 es 4000
        }

        [Theory]
        [InlineData(8000, 800)]
        [InlineData(10000, 1500)]
        [InlineData(18500, 2775)]
        [InlineData(20000, 4000)]
        [InlineData(30000, 6000)]
        public async Task CalcularDescuento_VariosValores_EsCorrecto(double montoCompra, double descuentoEsperado)
        {
            string url = $"https://iso-uncaus.somee.com/iso/Test/CalcularDescuento/{montoCompra}";
            double result = await RealizarSolicitudAsync(url);
            Assert.Equal(descuentoEsperado, result);
        }
    }
}
