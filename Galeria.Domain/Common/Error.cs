namespace Galeria.Domain.Common;

/// <summary>
/// Error de negocio: un código estable (para lógica/tests) y un mensaje (para el usuario).
/// Es un record: inmutable y con igualdad por valor.
/// </summary>
public sealed record Error(string Codigo, string Mensaje)
{
    /// <summary>Representa la ausencia de error (usado en resultados exitosos).</summary>
    public static readonly Error Ninguno = new(string.Empty, string.Empty);
}
