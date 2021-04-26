using Microsoft.EntityFrameworkCore;
using module_10.DAL.DataAccess;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace module_10.DAL.Repositories
{
    public class LectionRepository : IRepository<Lection>
    {
        private readonly DataContext _db;

        public LectionRepository(DataContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Lection>> GetAllAsync()
        {
            var lection = await _db.Lections.ToListAsync();
            return lection;
        }

        public async Task<Lection> GetAsync(int? id)
        {
            var lection = await _db.Lections.FindAsync(id);
            return lection;
        }

        public async Task CreateAsync(Lection lection)
        {
            await _db.Lections.AddAsync(lection);
            await _db.SaveChangesAsync();
        }

        public void Update(Lection lection)
        {
            _db.Entry(lection).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Lection> Find(Func<Lection, bool> predicate)
        {
            var lection = _db.Lections
                .Where(predicate)
                .ToList();
            return lection;
        }

        public void Delete(Lection lection)
        {
            _db.Lections.Remove(lection);
            _db.SaveChanges();
        }
    }
}
