﻿using EgeBilgiTaskCase.Domain.Entities.Character;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Common
{
    public class LocationWriteRepository : WriteRepository<Location>, ILocationWriteRepository
    {
        public LocationWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
