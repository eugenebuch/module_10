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
            var Lection = await _db.Lections.ToListAsync();
            return Lection;
        }

        public async Task<Lection> GetAsync(int? id)
        {
            var Lection = await _db.Lections.FindAsync(id);
            return Lection;
        }

        public async Task CreateAsync(Lection Lection)
        {
            await _db.Lections.AddAsync(Lection);
            await _db.SaveChangesAsync();
        }

        public void Update(Lection Lection)
        {
            _db.Entry(Lection).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Lection> Find(Func<Lection, bool> predicate)
        {
            var Lection = _db.Lections
                .Where(predicate)
                .ToList();
            return Lection;
        }

        public void Delete(Lection Lection)
        {
            _db.Lections.Remove(Lection);
            _db.SaveChanges();
        }
    }
}
