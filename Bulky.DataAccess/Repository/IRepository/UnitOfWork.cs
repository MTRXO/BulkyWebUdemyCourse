using Bulky.DataAcces.Data;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDBContext _db;
        public ICategoryRepository Category { get; set; }

        public UnitOfWork(ApplicationDBContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
        }

        public void Save()
        {
            throw new NotImplementedException();    
        }
    }
}
