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
            var lecturer = await _db.Lecturers.ToListAsync();
            return lecturer;
        }

        public async Task<Lecturer> GetAsync(int? id)
        {
            var lecturer = await _db.Lecturers.FindAsync(id);
            return lecturer;
        }

        public async Task CreateAsync(Lecturer lecturer)
        {
            await _db.Lecturers.AddAsync(lecturer);
            await _db.SaveChangesAsync();
        }

        public void Update(Lecturer lecturer)
        {
            _db.Entry(lecturer).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Lecturer> Find(Func<Lecturer, bool> predicate)
        {
            var lecturer = _db.Lecturers
                .Where(predicate)
                .ToList();
            return lecturer;
        }

        public void Delete(Lecturer lecturer)
        {
            _db.Lecturers.Remove(lecturer);
            _db.SaveChanges();
        }
    }
}
