﻿using EgeBilgiTaskCase.Application.Repositories.Management;
using EgeBilgiTaskCase.Domain.Entities.Management;
using EgeBilgiTaskCase.Persistence.Context;

namespace EgeBilgiTaskCase.Persistence.Repositories.Management
{
    public class ItemChangeWriteRepository : WriteRepository<ItemChange>, IItemChangeWriteRepository
    {
        public ItemChangeWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
