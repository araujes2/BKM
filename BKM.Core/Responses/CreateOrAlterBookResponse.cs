using BKM.Core.DTO;
using BKM.Core.Generic;

namespace BKM.Core.Responses
{
    public class CreateOrAlterBookResponse : ResponseBase
    {
        public DtoBook Result { get; set; }
    }
}
