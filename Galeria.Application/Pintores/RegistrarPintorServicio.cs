using Galeria.Domain.Common;
using Galeria.Domain.Entities;
using Galeria.Domain.Errors;
using Galeria.Domain.Interfaces;

namespace Galeria.Application.Pintores;

/// <summary>Caso de uso: registrar un pintor nuevo.</summary>
public sealed class RegistrarPintorServicio(IPintorRepositorio repositorio)
{
    public Result<Guid> Ejecutar(RegistrarPintorDto dto)
    {
        // Regla de negocio que necesita el repositorio → vive en Application.
        if (repositorio.ExisteConNombre(dto.Nombre))
            return PintorErrores.Duplicado(dto.Nombre);

        // Reglas propias de la entidad → viven en Domain.
        var resultado = Pintor.Crear(dto.Nombre, dto.Nacionalidad, dto.AnioNacimiento);
        if (resultado.EsFallo)
            return resultado.Error;   // propagamos el error tal cual

        repositorio.Agregar(resultado.Valor);
        return resultado.Valor.Id;
    }
}
