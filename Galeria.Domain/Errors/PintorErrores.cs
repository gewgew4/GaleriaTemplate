using Galeria.Domain.Common;

namespace Galeria.Domain.Errors;

/// <summary>Catálogo centralizado de errores de negocio del agregado Pintor.</summary>
public static class PintorErrores
{
    public static readonly Error NombreVacio =
        new("Pintor.NombreVacio", "El nombre del pintor es obligatorio.");

    public static Error AnioInvalido(int anio) =>
        new("Pintor.AnioInvalido", $"El año de nacimiento {anio} no es válido.");

    public static Error Duplicado(string nombre) =>
        new("Pintor.Duplicado", $"Ya existe un pintor llamado '{nombre}'.");
}
