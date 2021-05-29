using BKM.Core.DTO;
using BKM.Core.Generic;

namespace BKM.Core.Responses
{
    public class CreateBookResponse : ResponseBase
    {
        public DtoBook Result { get; set; }
    }
}
