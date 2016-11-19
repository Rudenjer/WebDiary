﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebDiary.DAL.Repository.Interfaces
{
    public interface IRepository<TEntity, in TKey>
    {
        TEntity Get(TKey id);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null);

        IEnumerable<TResult> Select<TResult>(Func<TEntity, TResult> selector);

        void Create(TEntity item);

        void Update(TEntity item);

        void Delete(TEntity item);
    }
}
