using BKM.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BKM.Core.Commands
{
    public class AlterBookCommandValidation
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly AlterBookCommand _command;
        private readonly ICollection<string> _errors;
        public AlterBookCommandValidation(IRepositoryProvider repositoryProvider, AlterBookCommand command)
        {
            _repositoryProvider = repositoryProvider;
            _command = command;
            _errors = new List<string>();
        }

        public string[] Errors => _errors.ToArray();

        public bool IsValid()
        {
            //Regex rgx = new Regex(@"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$");

            //if (rgx.IsMatch(_command.ISBM) == false)
            //{
            //    _errors.Add("Invalid ISBM");
            //}

            if (string.IsNullOrEmpty(_command.ISBM))
            {
                _errors.Add("ISBM is empty");
            }

            if (string.IsNullOrEmpty(_command.Title))
            {
                _errors.Add("Name is empty");
            }

            if (_repositoryProvider.Author.Load().FirstOrDefault(m => m.ID == _command.AuthorID) == null)
            {
                _errors.Add("Author Not Found");
            }

            if (_repositoryProvider.Book.Load().Any(m => m.ISBM == _command.ISBM) == false)
            {
                _errors.Add("Book does not exist");
            }

            return _errors.Count == 0;
        }
    }
}
