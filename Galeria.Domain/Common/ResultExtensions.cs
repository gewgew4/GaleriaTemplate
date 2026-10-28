namespace Galeria.Domain.Common;

public static class ResultExtensions
{
    /// <summary>Si hay éxito, transforma el valor. Si hay fallo, propaga el error.</summary>
    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> resultado, Func<TIn, TOut> transformar) =>
        resultado.EsExito
            ? Result.Exito(transformar(resultado.Valor))
            : Result.Fallo<TOut>(resultado.Error);

    /// <summary>Si hay éxito, ejecuta la siguiente operación (que también puede fallar).</summary>
    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> resultado, Func<TIn, Result<TOut>> siguiente) =>
        resultado.EsExito
            ? siguiente(resultado.Valor)
            : Result.Fallo<TOut>(resultado.Error);

    /// <summary>Consume el resultado obligando a contemplar ambos caminos.</summary>
    public static TOut Match<TIn, TOut>(this Result<TIn> resultado, Func<TIn, TOut> siExito, Func<Error, TOut> siFallo) =>
        resultado.EsExito ? siExito(resultado.Valor) : siFallo(resultado.Error);
}
