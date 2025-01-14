﻿using RecordShop.Model;
using RecordShop.Services.Generic;

namespace RecordShop.Services
{
    public interface IRecordsService : IGenericMappingService<Record, RecordDTO>
    {
    }
}
