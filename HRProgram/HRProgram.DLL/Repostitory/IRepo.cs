﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProgram.DLL.Repostitory
{
    public interface IRepo<T> where T: class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void CreateOrUpdate(T entity);
        void Delete(T entity);
        void SaveChanges();
    }
}
