using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Application.Shared
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        // Injetamos todos os validadores que o FluentValidation encontrou
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Se não houver nenhum validador para este tipo de comando, apenas continue.
            if (!_validators.Any())
            {
                return await next();
            }

            // Cria um contexto de validação
            var context = new ValidationContext<TRequest>(request);

            // Executa todos os validadores em paralelo e coleta os resultados
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // Pega todos os erros de todos os validadores e os agrupa em uma única lista
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            // Se houver alguma falha de validação...
            if (failures.Any())
            {
                // ...lança uma exceção de validação contendo todos os erros.
                // O nosso GlobalExceptionHandler irá capturar esta exceção e retornar um HTTP 400.
                throw new ValidationException(failures);
            }

            // Se não houver falhas, continua para o próximo passo na pipeline (o Handler).
            return await next();
        }
    }
}
