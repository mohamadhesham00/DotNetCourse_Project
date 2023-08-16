﻿using DotNet.DataAccess.Data;
using DotNet.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository category { get; private set; } 

        private readonly ApplicationDbContext _db;


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            category = new CategoryRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
