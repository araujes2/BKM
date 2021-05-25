using BKM.Core.DTO;
using BKM.Core.Generic;

namespace BKM.Core.Generic
{
    public class CreateAuthorResponse : CommandResponseBase
    {
        public DtoAuthor Result { get; set; }
    }
}
