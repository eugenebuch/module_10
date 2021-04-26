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
    public class LecturerRepository : IRepository<Lecturer>
    {
        private readonly DataContext _db;

        public LecturerRepository(DataContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Lecturer>> GetAllAsync()
        {
            var Lecturer = await _db.Lecturers.ToListAsync();
            return Lecturer;
        }

        public async Task<Lecturer> GetAsync(int? id)
        {
            var Lecturer = await _db.Lecturers.FindAsync(id);
            return Lecturer;
        }

        public async Task CreateAsync(Lecturer Lecturer)
        {
            await _db.Lecturers.AddAsync(Lecturer);
            await _db.SaveChangesAsync();
        }

        public void Update(Lecturer Lecturer)
        {
            _db.Entry(Lecturer).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Lecturer> Find(Func<Lecturer, bool> predicate)
        {
            var Lecturer = _db.Lecturers
                .Where(predicate)
                .ToList();
            return Lecturer;
        }

        public void Delete(Lecturer Lecturer)
        {
            _db.Lecturers.Remove(Lecturer);
            _db.SaveChanges();
        }
    }
}
