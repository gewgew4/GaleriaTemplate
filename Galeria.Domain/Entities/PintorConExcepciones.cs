namespace Galeria.Domain.Entities;

/// <summary>
/// Versión "clásica": valida lanzando excepciones.
/// Se mantiene solo para comparar en el benchmark.
/// </summary>
public sealed class PintorConExcepciones
{
    public PintorConExcepciones(string nombre, string nacionalidad, int anioNacimiento)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del pintor es obligatorio.", nameof(nombre));

        if (anioNacimiento < 1200 || anioNacimiento > DateTime.Today.Year)
            throw new ArgumentOutOfRangeException(nameof(anioNacimiento),
                $"El año de nacimiento {anioNacimiento} no es válido.");

        Id = Guid.NewGuid();   // mismo trabajo que Pintor, para que el benchmark sea justo
        Nombre = nombre.Trim();
        Nacionalidad = nacionalidad.Trim();
        AnioNacimiento = anioNacimiento;
    }

    public Guid Id { get; }
    public string Nombre { get; }
    public string Nacionalidad { get; }
    public int AnioNacimiento { get; }
}
