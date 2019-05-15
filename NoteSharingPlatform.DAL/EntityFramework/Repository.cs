﻿using NoteSharingPlatform.DAL;
using NoteSharingPlatform.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NoteSharingPlatform.DAL.EntityFramework
{
    public class Repository<T> :RepositoryBase , IRepository<T> where T: class
    {
        private NSPContext _db;
        private DbSet<T> _objectSet;

        public Repository()
        {
            _db = new NSPContext();
            _objectSet = _db.Set<T>();
        }

        public List<T> List()
        {
            return _objectSet.ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public List<T> List(Expression<Func<T,bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);
            return Save();
        }

        public int Update(T obj)
        {
            return Save();
        }

        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }

        public int Save()
        {
            return _db.SaveChanges();
        }
    }
}
