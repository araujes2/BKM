using BKM.Core.Commands;
using BKM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BKM.Core.Commands
{
    public class CreateAuthorCommandValidation
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly CreateAuthorCommand _command;
        private readonly ICollection<string> _errors;
        public CreateAuthorCommandValidation(IRepositoryProvider repositoryProvider, CreateAuthorCommand command)
        {
            _repositoryProvider = repositoryProvider;
            _command = command;
            _errors = new List<string>();
        }

        public string[] Errors => _errors.ToArray();

        public bool IsValid()
        {
            if (IsCPF(_command.ID) == false)
            {
                _errors.Add("Invalid ID");
            }

            if (string.IsNullOrEmpty(_command.Name))
            {
                _errors.Add("Name is empty");
            }

            if(_command.DateOfBirth > _command.Today.Value.AddMonths(-360))
            {
                _errors.Add("Date of Birth is lower 30 years old");
            }

            if (_repositoryProvider.Author.Load().Any(m => m.ID == _command.ID))
            {
                _errors.Add("Author already exists");
            }

            return _errors.Count == 0;
        }

        private bool IsCPF(string CPF)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            CPF = CPF.Trim();
            CPF = CPF.Replace(".", "").Replace("-", "");
            if (CPF.Length != 11)
                return false;
            tempCpf = CPF.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return CPF.EndsWith(digito);
        }
    }
}
