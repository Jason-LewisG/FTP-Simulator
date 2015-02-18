﻿using System.Collections.Generic;
using System.Data.Entity;
using Stardome.DomainObjects;
using Stardome.Infrastructure.Repository;

namespace Stardome.Repositories
{
    public interface IUserAuthCredentialRepository : IObjectRepository<UserAuthCredential>
    {
        UserAuthCredential GetByUsername(string username);
        IEnumerable<UserAuthCredential> GetAll();
    }
}