using Galeria.Domain.Entities;
using Galeria.Domain.Interfaces;

namespace Galeria.Infrastructure.Repositorios;

public sealed class PintorRepositorioEnMemoria : IPintorRepositorio
{
    private readonly List<Pintor> _pintores = [];

    public bool ExisteConNombre(string nombre) =>
        _pintores.Any(p => string.Equals(p.Nombre, nombre.Trim(), StringComparison.OrdinalIgnoreCase));

    public void Agregar(Pintor pintor) => _pintores.Add(pintor);

    public IReadOnlyList<Pintor> ObtenerTodos() => _pintores.AsReadOnly();
}
