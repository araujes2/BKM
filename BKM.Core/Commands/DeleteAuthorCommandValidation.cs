using BKM.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BKM.Core.Commands
{
    public class DeleteAuthorCommandValidation
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly DeleteAuthorCommand _command;
        private readonly ICollection<string> _errors;
        public DeleteAuthorCommandValidation(IRepositoryProvider repositoryProvider, DeleteAuthorCommand command)
        {
            _repositoryProvider = repositoryProvider;
            _command = command;
            _errors = new List<string>();
        }

        public string[] Errors => _errors.ToArray();

        public bool IsValid()
        {
            var author = _repositoryProvider.Author.Load().FirstOrDefault(m => m.ID == _command.ID);

            if (author == null)
            {
                _errors.Add("Author Not Found");
            }
            else if (author.Books.Any())
            {
                _errors.Add("Cannot remove author with books");
            }

            return _errors.Count == 0;
        }

    }
}
