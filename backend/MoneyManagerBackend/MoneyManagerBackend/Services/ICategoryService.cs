﻿using MoneyManagerBackend.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManagerBackend.Services
{
    public interface ICategoryService
    {
        public List<Category> GetCategories();
    }
}