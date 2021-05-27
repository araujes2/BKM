using BKM.Core.DTO;
using BKM.Core.Generic;

namespace BKM.Core.Commands
{
    public class CreateAuthorResponse : ResponseBase
    {
        public DtoAuthor Result { get; set; }
    }
}
