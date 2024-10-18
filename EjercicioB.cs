namespace Trabajo_Practico___Pruebas_de_software
{
    public class EjercicioB : HttpClientBase
    {
        private async Task<double> Solicitud(string url)
        {
            string responseBody = await base.RealizarSolicitud(url);
            return double.Parse(responseBody);
        }

        [Theory]
        [InlineData(0.001, 0.0001)]
        [InlineData(0.01, 0.001)]
        [InlineData(0.1, 0.01)]
        [InlineData(0, 0)]
        [InlineData(1, 0.1)]
        [InlineData(10, 1)]
        [InlineData(100, 10)]
        [InlineData(1000, 100)]
        [InlineData(9999, 999.9)]
        [InlineData(9999.9, 999.99)]
        [InlineData(9999.99, 999.999)]
        [InlineData(9999.999, 999.9999)]
        public async Task Descuento10Porciento(double montoCompra, double descuentoEsperado)
        {
            string url = $"https://iso-uncaus.somee.com/iso/Test/CalcularDescuento/{montoCompra}";
            double result = await Solicitud(url);
            Assert.Equal(descuentoEsperado, result);
        }

        [Theory]
        [InlineData(10000, 1500)]
        [InlineData(15555, 2333.25)]
        [InlineData(19999.9, 2999.985)]
        [InlineData(19999.99, 2999.9985)]
        [InlineData(19999.999, 2999.99985)]
        public async Task Descuento15Porciento(double montoCompra, double descuentoEsperado)
        {
            string url = $"https://iso-uncaus.somee.com/iso/Test/CalcularDescuento/{montoCompra}";
            double result = await Solicitud(url);
            Assert.Equal(descuentoEsperado, result);
        }

        [Theory]
        [InlineData(20000, 4000)]
        [InlineData(30000, 6000)]
        [InlineData(100000, 20000)]
        [InlineData(1000000, 200000)]
        [InlineData(54321, 10864.2)]
        [InlineData(65432.1, 13086.42)]
        [InlineData(76543.21, 15308.642)]
        [InlineData(87654.321, 17530.8642)]
        public async Task Descuento20Porciento(double montoCompra, double descuentoEsperado)
        {
            string url = $"https://iso-uncaus.somee.com/iso/Test/CalcularDescuento/{montoCompra}";
            double result = await Solicitud(url);
            Assert.Equal(descuentoEsperado, result);
        }


        [Theory]
        [InlineData(-30000, 0)]
        [InlineData(-20000, 0)]
        [InlineData(-10000, 0)]
        [InlineData(-1, 0)]
        [InlineData(-0.9, 0)]
        [InlineData(-0.99, 0)]
        [InlineData(-0.999, 0)]
        public async Task DescuentosInvalidos(double montoCompra, double descuentoEsperado)
        {
            string url = $"https://iso-uncaus.somee.com/iso/Test/CalcularDescuento/{montoCompra}";
            double result = await Solicitud(url);
            Assert.Equal(descuentoEsperado, result);
        }
    }
}
