using Galeria.Domain.Common;

namespace Galeria.Domain.Errors;

public static class ClienteErrores
{
    public static readonly Error NombreVacio =
        new("Cliente.NombreVacio", "El nombre del cliente es obligatorio.");

    public static Error EmailInvalido(string email) =>
        new("Cliente.EmailInvalido", $"El email '{email}' no es válido.");
}
