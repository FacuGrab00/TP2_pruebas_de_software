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

        public static IEnumerable<object[]> ObtenerDatosGanancia()
        {
            yield return new object[] { new DatosImpuestoGanancia { IngresosBrutos = 50000, AportesJubilatorios = 5000, ObraSocial = 2000, CuotasMedicoAsistenciales = 3000, Alquileres = 4000, SegurosDeVida = 1000, Donaciones = 500, GastosMovilidad = 600, MinimoNoImponible = 7000, DeduccionEspecial = 5000, CargasFamilia = 2000 }, 19900 };
            yield return new object[] { new DatosImpuestoGanancia { IngresosBrutos = 30000 }, 30000 }; // Caso sin deducciones
            yield return new object[] { new DatosImpuestoGanancia { IngresosBrutos = 10000, AportesJubilatorios = 1000, ObraSocial = 500, CuotasMedicoAsistenciales = 300, Alquileres = 200, SegurosDeVida = 100 }, 6900 }; // Caso con deducciones parciales
            yield return new object[] { new DatosImpuestoGanancia { IngresosBrutos = 5000, AportesJubilatorios = 1000, ObraSocial = 500, CuotasMedicoAsistenciales = 300, Alquileres = 200, SegurosDeVida = 100 }, 1900 }; // Caso con deducciones que no generan negativo
            yield return new object[] { new DatosImpuestoGanancia { IngresosBrutos = 1000, AportesJubilatorios = 1000, ObraSocial = 1000, CuotasMedicoAsistenciales = 1000, Alquileres = 1000, SegurosDeVida = 1000, Donaciones = 1000, GastosMovilidad = 1000 }, -9000 }; // Deducciones mayores o iguales que el ingreso
        }


        [Theory]
        [MemberData(nameof(ObtenerDatosGanancia))]
        async public void CalcularImpuestoGanancia_ValoresVálidos_CalculoCorrecto(DatosImpuestoGanancia ganancia, decimal esperado)
        {
            string url = "https://iso-uncaus.somee.com/iso/Test/CalcularGanancia";

            decimal resultado = await Solicitud(ganancia, url);

            Assert.Equal(esperado, resultado);
        }
    }
}
