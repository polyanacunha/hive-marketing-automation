using Hive.Application.Interfaces;
using Hive.Domain.Entities;
using System.Text;

namespace Hive.Application.Services.ProcessPrompt
{
    public class PromptProcessor : IPromptVideoProcessor
    {
        public Task<(string promptSystem, string promptUser)> ContextualizePromptToCreateVideo(ClientProfile client, string clientObservations)
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
                - "visual_prompt": Uma descrição detalhada e visual da cena, em inglês, otimizada para uma IA de geração de vídeo (como Midjourney ou Pika Labs). Use palavras-chave como ângulos de câmera (close-up, wide shot), iluminação (cinematic, golden hour) e estilo (hyperrealistic, animated).
                - "on_screen_text": Qualquer texto que deva aparecer na tela durante a cena (string vazia se não houver).
                - "narrator_script": O texto exato para o narrador durante a cena em português do Brasil (string vazia se não houver).

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
    }
}
