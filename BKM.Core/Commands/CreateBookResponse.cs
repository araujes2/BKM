using BKM.Core.DTO;
using BKM.Core.Generic;

namespace BKM.Core.Commands
{
    public class CreateBookResponse : ResponseBase
    {
        public DtoBook Result { get; set; }
    }
}
