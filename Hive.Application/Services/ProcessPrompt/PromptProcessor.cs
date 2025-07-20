using Hive.Application.DTOs;
using Hive.Application.Interfaces;
using Hive.Domain.Entities;
using System.Text;
using System.Text.Json;

namespace Hive.Application.Services.ProcessPrompt
{
    public class PromptProcessor : IPromptProcessor
    {
        public Task<T> DeserializeJson<T>(string json)
        {
            var cleanJson = json.Trim();

            // Remove o ```json do início e o ``` do final
            if (cleanJson.StartsWith("```json"))
            {
                cleanJson = cleanJson.Substring("```json".Length);
            }
            if (cleanJson.StartsWith("```"))
            {
                cleanJson = cleanJson.Substring("```".Length);
            }
            if (cleanJson.EndsWith("```"))
            {
                cleanJson = cleanJson.Substring(0, cleanJson.Length - "```".Length);
            }
            // =======================================================

            // 2. Agora, desserializa a string JÁ LIMPA
            var script = JsonSerializer.Deserialize<T>(cleanJson.Trim());

            if (script is null)
            {
                throw new Exception("A IA retornou um roteiro em formato JSON inválido após a limpeza.");
            }

            return Task.FromResult(script);
        }

        public Task<(string promptSystem, string promptUser)> PromptToCreateVideo(ClientProfile client, string clientObservations)
        {
            var promptUser = new StringBuilder();

            var promptSystem = "## 1. SEU PAPEL:  Você é um roteirista de publicidade especialista em marketing digital e um especialista em engenharia de prompt para IAs de geração de vídeo.";

            promptUser.Append($$"""
                # INSTRUÇÕES PARA A IA:

                ## 1. SUA TAREFA:
                Sua tarefa é criar um roteiro para um vídeo de aproximadamente 30 segundos. Você DEVE retornar sua resposta como um objeto JSON. O objeto JSON deve conter uma chave "script" que é um array de objetos, onde cada objeto representa uma cena do vídeo.
                Cada objeto de cena deve ter as seguintes chaves:
                - "scene_number": O número da cena (começando em 1).
                - "duration_seconds": A duração estimada da cena em segundos.
                - "prompt": Uma descrição clara e detalhada da cena, em inglês, incluindo: quem é o personagem, o que ele faz (com posição e objeto), cenário, tipo de iluminação, e a fala ou narração em português ressaltando o tom de voz escolhido. 
               
                ## 2. CONTEXTO DO CLIENTE:
                - **Nome da Empresa:** {{client.CompanyName}}
                - **Segmento de Mercado:** {{client.MarketSegment.Description}}
                - **Público-Alvo:** {{client.TargetAudience.Description}}
                - **Faixa Etária do Público:** 

                ## 3. IDEIA CENTRAL DO VÍDEO:
                "{{clientObservations}}", caso nao esteja preenchido gerar ideia

                ## 4. INSTRUÇÕES GERAIS:
                - **Tom de Voz:** Enérgico e Jovem
                - **Estilo Visual:** Moderno e minimalista

                ## 5. RESPOSTA JSON:
                """);

            var result = promptUser.ToString();

            return Task.FromResult((promptSystem, result));
        }

        public Task<(string promptSystem, string promptUser)> PromptToGenerateTargeting(ClientProfile client, Campaign campaign)
        {
            var promptUser = new StringBuilder();

            var promptSystem = "SEU PAPEL:Você é um Estrategista de Mídia Digital especialista em execução e otimização de campanhas de alta performance em plataformas de anúncios pagos como Meta Ads, Google Ads, TikTok Ads";

            promptUser.Append($$"""
                Com base nas informações da empresa e da campanha, gere uma estratégia completa em formato JSON, usando exatamente esta estrutura:

                {
                  "targeting_strategy": {
                    "audience": {
                      "min_age": 0,
                      "max_age": 0,
                      "genders": [],
                      "interests": [],
                      "locations": []
                    },
                    "placements_suggestion": [],
                    "schedule_suggestion": {
                      "start_time": "",
                      "daily_hours": ""
                    }
                  },
                  "creative_suggestions": {
                    "suggested_format": "",
                    "central_theme": "",
                    "headlines": ["", "", ""],
                    "descriptions": ["", "", ""],
                    "calls_to_action": ["", "", ""],
                    "hashtags": []
                  },
                  "strategy_justification": ""
                }

                **Regras**:
                - Use no máximo 3 interesses no campo "interests"
                - Responda em português apenas com o JSON preenchido. Nenhum comentário ou explicação externa.

                **Dados de entrada (campos nulos não leve em consideração)**:
                - Empresa: {{client.CompanyName}}  
                - Segmento: {{client.MarketSegment.Description}}  
                - Site: {{client.WebSiteUrl}}
                - Público: {{client.TargetAudience.Description}}  
                - Objetivo: {{campaign.ObjectiveCampaign.Name}}  
                - Orçamento: {{campaign.Budget}}
                """);
             
            var result = promptUser.ToString();

            return Task.FromResult((promptSystem, result));
        }
    }
}
//-Mídias sociais: { { client.SocialMediaLinks } }
//-Telefone: { { client.Phone } }
//-Produto: { { campaign.ProductDescription } }
//-Proposta de valor: { { campaign.ValueProposition } }