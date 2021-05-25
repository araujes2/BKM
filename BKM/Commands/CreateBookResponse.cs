using BKM.Core.DTO;
using BKM.Core.Generic;

namespace BKM.API
{
    public class CreateBookResponse : ResponseBase
    {
        public DtoBook Result { get; set; }
    }
}
