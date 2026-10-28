using Galeria.Domain.Common;
using Galeria.Domain.Errors;

namespace Galeria.Domain.Entities;

public sealed class Pintor
{
    public const int AnioMinimo = 1200;

    // Constructor privado: la única forma de crear un Pintor es pasando por las validaciones.
    private Pintor(string nombre, string nacionalidad, int anioNacimiento)
    {
        Id = Guid.NewGuid();
        Nombre = nombre;
        Nacionalidad = nacionalidad;
        AnioNacimiento = anioNacimiento;
    }

    public Guid Id { get; }
    public string Nombre { get; }
    public string Nacionalidad { get; }
    public int AnioNacimiento { get; }

    /// <summary>Factory method: la firma avisa que la creación puede fallar.</summary>
    public static Result<Pintor> Crear(string nombre, string nacionalidad, int anioNacimiento)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return PintorErrores.NombreVacio;

        if (anioNacimiento < AnioMinimo || anioNacimiento > DateTime.Today.Year)
            return PintorErrores.AnioInvalido(anioNacimiento);

        return new Pintor(nombre.Trim(), nacionalidad.Trim(), anioNacimiento);
    }
}
