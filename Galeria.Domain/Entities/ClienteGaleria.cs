using System.Net.Mail;
using Galeria.Domain.Common;
using Galeria.Domain.Errors;

namespace Galeria.Domain.Entities;

public sealed class ClienteGaleria
{
    private ClienteGaleria(string nombre, string email)
    {
        Id = Guid.NewGuid();
        Nombre = nombre;
        Email = email;
    }

    public Guid Id { get; }
    public string Nombre { get; }
    public string Email { get; }

    public static Result<ClienteGaleria> Crear(string nombre, string email) =>
        ValidarNombre(nombre)
            .Bind(nombreValido => ValidarEmail(email)
                .Map(emailValido => new ClienteGaleria(nombreValido, emailValido)));

    private static Result<string> ValidarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return ClienteErrores.NombreVacio;
        return nombre.Trim();
    }

    private static Result<string> ValidarEmail(string email)
    {
        if (!MailAddress.TryCreate(email, out var direccion))
            return ClienteErrores.EmailInvalido(email);
        return direccion.Address;
    }
}
