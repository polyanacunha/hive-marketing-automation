using Hive.Application.DTOs;

namespace Hive.Application.Interfaces;

public interface IMidiaService
{

    /// <summary>
    /// Retorna uma mídia pelo seu identificador.
    /// </summary>
    /// <exception cref="KeyNotFoundException">Se o produto não existir.</exception>
    Task<MidiaDTO> GetByIdAsync(int id);

    /// <summary>
    /// Atualiza uma mídia existente.
    /// </summary>
    /// <exception cref="KeyNotFoundException">Se o produto não existir.</exception>
    Task UpdateAsync(MidiaDTO midiaDto);

    /// <summary>
    /// Remove uma mídia existente.
    /// </summary>
    /// <exception cref="KeyNotFoundException">Se o produto não existir.</exception>
    Task RemoveAsync(int id);
}