﻿using ECommerce.Persistance.Contexts;
using ECommerce.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Repositories.File
{
    public class FileReadRepository : ReadRepository<Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
