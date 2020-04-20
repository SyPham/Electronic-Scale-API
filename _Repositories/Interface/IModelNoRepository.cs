﻿using EC_API.Data;
using EC_API.DTO;
using EC_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_API._Repositories.Interface
{
    public interface IModelNoRepository : IECRepository<ModelNo>
    {
        //Task<List<GlueCreateDto>> GetModelNoByModelNameID(int modelNameID);
        Task<object> GetModelNoByModelNameID(int modelNameID);
    }
}
