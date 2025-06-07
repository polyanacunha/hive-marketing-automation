using Hive.Application.DTOs;

namespace Hive.Application.Interfaces;

public interface ICampaingService
{
    /// <summary>
    /// Retorna uma lista de campanhas.
    /// </summary>
    Task<IEnumerable<CampaingDTO>> GetCampaings();

    /// <summary>
    /// Retorna uma campanha pelo seu identificador.
    /// </summary>
    Task<CampaingDTO> GetById(int? id);

    /// <summary>
    /// Adiciona uma nova campanha.
    /// </summary>
    Task Add(CampaingDTO campaingDTO);

    /// <summary>
    /// Atualiza uma campanha.
    /// </summary>
    Task Update(CampaingDTO campaingDTO);

    /// <summary>
    /// Remove uma campanha.
    /// </summary>
    Task Remove(int? id);
}
