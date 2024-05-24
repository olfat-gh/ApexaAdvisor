﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Common.Interfaces
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

    }
}
