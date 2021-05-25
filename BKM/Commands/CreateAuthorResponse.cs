using BKM.Core.DTO;
using BKM.Core.Generic;

namespace BKM.API
{
    public class CreateAuthorResponse : ResponseBase
    {
        public DtoAuthor Result { get; set; }
    }
}
