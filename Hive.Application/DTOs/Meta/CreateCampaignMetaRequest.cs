using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.DTOs.Meta
{
    public record CreateCampaignMetaRequest(
        string AccountIdMeta,
        string? PageId,
        string? PixelId,
        string? InstagramActorId,
        string? ApplicationId,
        string? ObjectStoreUrl,
        string? FormId,
        string? PrivacyPolicyUrl,
        string? ProductCatalogId,
        string? ProductSetId)
    {
    }
}
