namespace Galeria.Domain.Common;

/// <summary>Resultado de una operación sin valor de retorno.</summary>
public class Result
{
    protected Result(bool esExito, Error error)
    {
        // Invariantes: un éxito no puede traer error y un fallo debe traerlo.
        if (esExito && error != Error.Ninguno)
            throw new InvalidOperationException("Un resultado exitoso no puede contener un error.");
        if (!esExito && error == Error.Ninguno)
            throw new InvalidOperationException("Un resultado fallido debe contener un error.");

        EsExito = esExito;
        Error = error;
    }

    public bool EsExito { get; }
    public bool EsFallo => !EsExito;
    public Error Error { get; }

    public static Result Exito() => new(true, Error.Ninguno);
    public static Result Fallo(Error error) => new(false, error);

    public static Result<T> Exito<T>(T valor) => new(valor, true, Error.Ninguno);
    public static Result<T> Fallo<T>(Error error) => new(default, false, error);
}

/// <summary>Resultado de una operación que, si sale bien, produce un valor de tipo T.</summary>
public sealed class Result<T> : Result
{
    private readonly T? _valor;

    internal Result(T? valor, bool esExito, Error error) : base(esExito, error)
        => _valor = valor;

    /// <summary>
    /// Acceder al valor de un fallo es un BUG del programador, no un error de negocio:
    /// por eso acá SÍ lanzamos excepción.
    /// </summary>
    public T Valor => EsExito
        ? _valor!
        : throw new InvalidOperationException("No se puede acceder al valor de un resultado fallido.");

    // Conversiones implícitas: permiten escribir `return pintor;` o `return PintorErrores.X;`
    public static implicit operator Result<T>(T valor) => Exito(valor);
    public static implicit operator Result<T>(Error error) => Fallo<T>(error);
}
