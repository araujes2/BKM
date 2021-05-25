using BKM.Core.DTO;
using BKM.Core.Generic;

namespace BKM.Core.Generic
{
    public class CreateBookResponse : CommandResponseBase
    {
        public DtoBook Result { get; set; }
    }
}
