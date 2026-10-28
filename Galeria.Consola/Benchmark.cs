using System.Diagnostics;
using Galeria.Domain.Entities;

/// <summary>Compara el costo de validar con excepciones vs. con Result.</summary>
internal static class Benchmark
{
    private const int Repeticiones = 5;

    /// <summary>Corre los dos escenarios (muchos errores / pocos errores) en una sola ejecución.</summary>
    public static void Ejecutar(int cantidad)
    {
        foreach (var porcentajeInvalidos in new[] { 50, 1 })
            EjecutarEscenario(cantidad, porcentajeInvalidos);
    }

    private static void EjecutarEscenario(int cantidad, int porcentajeInvalidos)
    {
        var datos = GenerarDatos(cantidad, porcentajeInvalidos);

        // Calentamiento: que el JIT compile ambos caminos antes de medir.
        ConExcepciones(datos[..1000]);
        ConResult(datos[..1000]);

        // Nos quedamos con la mejor de varias corridas para reducir el ruido de la máquina.
        var tiempoExc = MejorTiempo(() => ConExcepciones(datos));
        var tiempoRes = MejorTiempo(() => ConResult(datos));

        Console.WriteLine($"\n--- Benchmark: {cantidad:N0} pintores, {porcentajeInvalidos}% inválidos (mejor de {Repeticiones}) ---");
        Console.WriteLine($"Excepciones: {tiempoExc.TotalMilliseconds,10:N1} ms");
        Console.WriteLine($"Result:      {tiempoRes.TotalMilliseconds,10:N1} ms");
        Console.WriteLine($"Relación:    {tiempoExc / tiempoRes,10:N1}x");
    }

    private static TimeSpan MejorTiempo(Func<int> operacion)
    {
        var mejor = TimeSpan.MaxValue;
        for (var i = 0; i < Repeticiones; i++)
        {
            GC.Collect();                    // que la basura de la corrida anterior no ensucie la medición
            GC.WaitForPendingFinalizers();

            var sw = Stopwatch.StartNew();
            operacion();
            sw.Stop();

            if (sw.Elapsed < mejor) mejor = sw.Elapsed;
        }
        return mejor;
    }

    private static (string Nombre, int Anio)[] GenerarDatos(int cantidad, int porcentajeInvalidos)
    {
        var random = new Random(42); // semilla fija → resultados reproducibles
        return Enumerable.Range(0, cantidad)
            .Select(i => random.Next(100) < porcentajeInvalidos
                ? ($"Pintor {i}", 3000)      // año inválido
                : ($"Pintor {i}", 1900))
            .ToArray();
    }

    private static int ConExcepciones((string Nombre, int Anio)[] datos)
    {
        var validos = 0;
        foreach (var (nombre, anio) in datos)
        {
            try
            {
                _ = new PintorConExcepciones(nombre, "Argentina", anio);
                validos++;
            }
            catch (ArgumentException) { /* inválido: se descarta */ }
        }
        return validos;
    }

    private static int ConResult((string Nombre, int Anio)[] datos)
    {
        var validos = 0;
        foreach (var (nombre, anio) in datos)
        {
            if (Pintor.Crear(nombre, "Argentina", anio).EsExito)
                validos++;
        }
        return validos;
    }
}
