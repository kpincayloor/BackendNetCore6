﻿using DataAccess.Repository;

namespace DataAccess
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;
        void Commit();
    }
}
