using FluentValidation;
using Hive.Domain.Enum;

namespace Hive.Application.UseCases.Campaigns.Meta.CreateCampaign
{
    public class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommand>
    {
        public CreateCampaignCommandValidator()
        {
            RuleFor(x => x.CampaignName).NotEmpty();
            RuleFor(x => x.Budget).NotEmpty();
            RuleFor(x => x.InitialDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
            RuleFor(x => x.ObjectiveCampaign).NotEmpty();
                            
            When( x => x.Platform == Platform.Meta && x.MetaConfig == null, () =>
            {
                RuleFor(x => x.MetaConfig)
                    .NotEmpty().WithMessage("PixelId é obrigatório para campanhas de vendas.");
            });

        
        }
    }           
}

