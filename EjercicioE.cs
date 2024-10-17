namespace Trabajo_Practico___Pruebas_de_software
{
    public class DatosImpuestoGanancia
    {
        public decimal IngresosBrutos { get; set; }
        public decimal AportesJubilatorios { get; set; }
        public decimal ObraSocial { get; set; }
        public decimal CuotasMedicoAsistenciales { get; set; }
        public decimal Alquileres { get; set; }
        public decimal SegurosDeVida { get; set; }
        public decimal Donaciones { get; set; }
        public decimal GastosMovilidad { get; set; }
        public decimal MinimoNoImponible { get; set; }
        public decimal DeduccionEspecial { get; set; }
        public decimal CargasFamilia { get; set; }
    }

    public class EjercicioE : HttpClientBase
    {
        private async Task<decimal> Solicitud(DatosImpuestoGanancia ganancia, string url)
        {
            try
            {
                string responseBody = await base.SolicitudGetConBody(ganancia, url);
                return decimal.Parse(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error en la solicitud: {e.Message}");
                return 0;
            }
        }

        [Theory]
        [InlineData(1000000, 100000, 50000, 20000, 10000, 5000, 5000, 0, 330000, 180000, 70000, 0)]                     // Ganancia Acumulada < 1,253,400
        [InlineData(2000000, 100000, 80000, 40000, 20000, 10000, 8000, 0, 330000, 180000, 100000, 256254)]              // Ganancia Acumulada entre 1,253,400 y 2,506,800
        [InlineData(3500000, 150000, 100000, 50000, 30000, 15000, 12000, 0, 330000, 180000, 120000, 516876)]            // Ganancia Acumulada entre 2,506,800 y 3,760,200
        [InlineData(4500000, 200000, 150000, 60000, 40000, 20000, 15000, 0, 330000, 180000, 150000, 876057)]            // Ganancia Acumulada entre 3,760,200 y 5,640,300
        [InlineData(8000000, 300000, 200000, 80000, 50000, 25000, 20000, 0, 330000, 180000, 200000, 2117466)]           // Ganancia Acumulada entre 5,640,300 y 11,280,600
        [InlineData(14000000, 400000, 250000, 100000, 60000, 30000, 25000, 0, 330000, 180000, 250000, 4142238)]         // Ganancia Acumulada entre 11,280,600 y 16,920,900
        [InlineData(20000000, 500000, 300000, 120000, 80000, 35000, 30000, 0, 330000, 180000, 300000, 6203633)]         // Ganancia Acumulada entre 16,920,900 y 25,381,350
        [InlineData(30000000, 600000, 400000, 150000, 100000, 40000, 35000, 0, 330000, 180000, 350000, 9735279)]        // Ganancia Acumulada entre 25,381,350 y 38,072,025
        [InlineData(45000000, 800000, 500000, 200000, 120000, 50000, 40000, 0, 330000, 180000, 400000, 14665204)]       // Ganancia Acumulada mayor a 38,072,025
        [InlineData(1000000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)]                                                          // Ganancia Acumulada < 1,253,400 (Solo Ingreso Bruto)
        async public void ImpuestosGananciaValidos(
        decimal ingresosBrutos,
        decimal aportesJubilatorios,
        decimal obraSocial,
        decimal cuotasMedicoAsistenciales,
        decimal alquileres,
        decimal segurosDeVida,
        decimal donaciones,
        decimal gastosMovilidad,
        decimal minimoNoImponible,
        decimal deduccionEspecial,
        decimal cargasFamilia,
        decimal esperado)
        {
            // Arrange
            DatosImpuestoGanancia ganancia = new DatosImpuestoGanancia
            {
                IngresosBrutos = ingresosBrutos,
                AportesJubilatorios = aportesJubilatorios,
                ObraSocial = obraSocial,
                CuotasMedicoAsistenciales = cuotasMedicoAsistenciales,
                Alquileres = alquileres,
                SegurosDeVida = segurosDeVida,
                Donaciones = donaciones,
                GastosMovilidad = gastosMovilidad,
                MinimoNoImponible = minimoNoImponible,
                DeduccionEspecial = deduccionEspecial,
                CargasFamilia = cargasFamilia
            };

            string url = "https://iso-uncaus.somee.com/iso/Test/CalcularGanancia";

            decimal resultado = await Solicitud(ganancia, url);

            Assert.Equal(esperado, resultado);
        }
    }
}
