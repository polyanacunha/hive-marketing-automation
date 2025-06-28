using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.ValueObjects
{
    public class TaxId
    {
        public string Id { get; set; }

        public TaxId(string id)
        {
            Id = id;
        }

        public static bool IsValid(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            // Remove caracteres não numéricos
            var numbersOnly = new string(cpf.Where(char.IsDigit).ToArray());

            // 1. Verifica o tamanho
            if (numbersOnly.Length != 11)
                return false;

            // 2. Verifica se todos os dígitos são iguais (ex: 111.111.111-11)
            if (AreAllDigitsEqual(numbersOnly))
                return false;

            // 3. Calcula e compara os dígitos verificadores
            try
            {
                var firstNineDigits = numbersOnly.Substring(0, 9);
                var firstCheckDigit = CalculateCheckDigit(firstNineDigits);
                var secondCheckDigit = CalculateCheckDigit(firstNineDigits + firstCheckDigit);

                // Compara os dígitos calculados com os dígitos reais do CPF
                return numbersOnly.EndsWith(firstCheckDigit.ToString() + secondCheckDigit.ToString());
            }
            catch
            {
                // Se ocorrer qualquer erro na conversão ou cálculo, o CPF é inválido
                return false;
            }
        }

        private static bool AreAllDigitsEqual(string cpf)
        {
            // Pega o primeiro caractere e cria uma string de 11 caracteres repetindo-o
            var comparison = new string(cpf[0], 11);
            return cpf == comparison;
        }

        private static int CalculateCheckDigit(string partialCpf)
        {
            var sum = 0;
            // O peso começa em (comprimento da string + 1)
            var weight = partialCpf.Length + 1;

            foreach (var digitChar in partialCpf)
            {
                sum += int.Parse(digitChar.ToString()) * weight;
                weight--;
            }

            var remainder = sum % 11;
            var checkDigit = 11 - remainder;

            // Se o resultado for 10 ou 11, o dígito verificador é 0
            return checkDigit >= 10 ? 0 : checkDigit;
        }
    }
}
