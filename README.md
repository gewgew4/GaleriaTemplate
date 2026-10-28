# GaleriaTemplate

Punto de partida de la clase "¿Excepción o Result?" (T123-22, UAI). .NET 9, consola, Clean Architecture liviana.

Ya está hecha la demo (`Pintor`, `Result`, registro en consola, benchmark) y el ejercicio guiado (`Map`, `Bind`, `Match`, `ClienteGaleria`). El ejercicio autónomo no está resuelto.

## Abrir

1. Visual Studio 2022 17.12 o superior, con el SDK de .NET 9.
2. Abrir `GaleriaTemplate.sln`.
3. Si `Galeria.Consola` no es el proyecto de inicio, clic derecho y "Establecer como proyecto de inicio".
4. F5. Tiene que listar los cuatro pintores de la demo y los tres clientes. `pintores.csv` se copia solo junto al ejecutable.

Tests de lo que ya está: `dotnet test` desde esta carpeta.

## Ejercicio autónomo: importador CSV

Implementá un importador de pintores desde un archivo CSV con formato `Nombre;Nacionalidad;AnioNacimiento`. El archivo de prueba es `Galeria.Consola/pintores.csv`.

1. Una línea inválida no debe detener la importación: seguir con la siguiente.
2. Al final, informar cuántos pintores se importaron y la lista de errores con su número de línea.
3. Errores por línea: cantidad de columnas incorrecta, año no numérico, y todos los de `Pintor.Crear` y duplicados.
4. Si el archivo no existe, decidí si es `Result` o excepción y justificalo en un comentario. Un `catch` vacío, o atrapar `Exception` dentro del importador, no sirve.
5. El importador va en **Application** (`Galeria.Application`). La lectura del archivo, en **Presentation** (`Galeria.Consola/Program.cs`, donde está el comentario del paso 3).

Con `pintores.csv`, el resultado esperado es **3 importados** (líneas 1, 2 y 8) y **5 errores** (líneas 3 a 7). Para que la línea 1 no salga como duplicado, el importador tiene que usar un repositorio propio: el de la demo ya registró a Benito Quinquela Martín.

**Bonus:** dos tests del importador en `Galeria.Tests`, verificando el código de error y el número de línea.
