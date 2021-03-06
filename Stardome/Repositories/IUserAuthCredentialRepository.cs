﻿using System.Collections.Generic;
using Stardome.DomainObjects;
using Stardome.Infrastructure.Repository;

namespace Stardome.Repositories
{
    public interface IUserAuthCredentialRepository : IObjectRepository<UserAuthCredential>
    {
        UserAuthCredential GetByUsername(string username);

        UserAuthCredential GetByEmail(string email);

        IEnumerable<UserAuthCredential> GetAll();
    }
}
