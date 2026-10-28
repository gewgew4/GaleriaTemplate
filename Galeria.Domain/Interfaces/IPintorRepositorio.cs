using Galeria.Domain.Entities;

namespace Galeria.Domain.Interfaces;

public interface IPintorRepositorio
{
    bool ExisteConNombre(string nombre);
    void Agregar(Pintor pintor);
    IReadOnlyList<Pintor> ObtenerTodos();
}
