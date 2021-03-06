﻿using System.Collections.Generic;
using Stardome.DomainObjects;
using Stardome.Infrastructure.Repository;
using Stardome.Models;

namespace Stardome.Repositories
{
    public interface IRoleRepository : IObjectRepository<Role>
    {
        IEnumerable<Role> GetAll();
    }
}
