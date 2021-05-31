using BKM.Core.Commands;
using BKM.Core.Generic;
using BKM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BKM.Core.Commands
{
    public class AlterAuthorCommandValidation
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly AlterAuthorCommand _command;
        private readonly ICollection<string> _errors;
        public AlterAuthorCommandValidation(IRepositoryProvider repositoryProvider, AlterAuthorCommand command)
        {
            _repositoryProvider = repositoryProvider;
            _command = command;
            _errors = new List<string>();
        }

        public string[] Errors => _errors.ToArray();

        public bool IsValid()
        {
            if (CPFValidation.IsValid(_command.ID) == false)
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

            if (_repositoryProvider.Author.Load().Any(m => m.ID == _command.ID) == false)
            {
                _errors.Add("Author does not exist");
            }

            return _errors.Count == 0;
        }

       
    }
}
