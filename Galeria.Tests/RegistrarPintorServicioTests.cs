using Galeria.Application.Pintores;
using Galeria.Domain.Entities;
using Galeria.Domain.Interfaces;
using Moq;

namespace Galeria.Tests;

public class RegistrarPintorServicioTests
{
    private readonly Mock<IPintorRepositorio> _repositorio = new();

    [Fact]
    public void Ejecutar_PintorDuplicado_DevuelveErrorYNoAgrega()
    {
        _repositorio.Setup(r => r.ExisteConNombre("Xul Solar")).Returns(true);
        var servicio = new RegistrarPintorServicio(_repositorio.Object);

        var resultado = servicio.Ejecutar(new("Xul Solar", "Argentina", 1887));

        Assert.Equal("Pintor.Duplicado", resultado.Error.Codigo);
        _repositorio.Verify(r => r.Agregar(It.IsAny<Pintor>()), Times.Never);
    }

    [Fact]
    public void Ejecutar_DatosValidos_AgregaYDevuelveId()
    {
        var servicio = new RegistrarPintorServicio(_repositorio.Object);

        var resultado = servicio.Ejecutar(new("Xul Solar", "Argentina", 1887));

        Assert.True(resultado.EsExito);
        _repositorio.Verify(r => r.Agregar(It.Is<Pintor>(p => p.Id == resultado.Valor)), Times.Once);
    }
}
