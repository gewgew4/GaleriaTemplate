using Galeria.Domain.Entities;
using Galeria.Domain.Errors;

namespace Galeria.Tests;

public class PintorTests
{
    [Fact]
    public void Crear_ConDatosValidos_DevuelveExito()
    {
        var resultado = Pintor.Crear("Xul Solar", "Argentina", 1887);

        Assert.True(resultado.EsExito);
        Assert.Equal("Xul Solar", resultado.Valor.Nombre);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Crear_ConNombreVacio_DevuelveErrorNombreVacio(string nombre)
    {
        var resultado = Pintor.Crear(nombre, "Argentina", 1887);

        Assert.True(resultado.EsFallo);
        Assert.Equal(PintorErrores.NombreVacio, resultado.Error);
    }

    [Theory]
    [InlineData(1000)]
    [InlineData(3000)]
    public void Crear_ConAnioFueraDeRango_DevuelveErrorAnioInvalido(int anio)
    {
        var resultado = Pintor.Crear("Xul Solar", "Argentina", anio);

        Assert.Equal("Pintor.AnioInvalido", resultado.Error.Codigo);
    }

    [Fact]
    public void Valor_DeResultadoFallido_LanzaInvalidOperationException()
    {
        var resultado = Pintor.Crear("", "Argentina", 1887);

        Assert.Throws<InvalidOperationException>(() => resultado.Valor);
    }
}
