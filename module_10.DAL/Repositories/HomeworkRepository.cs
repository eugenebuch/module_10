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
    public class HomeworkRepository : IRepository<Homework>
    {
        private readonly DataContext _db;

        public HomeworkRepository(DataContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Homework>> GetAllAsync()
        {
            var homework = await _db.Homeworks.ToListAsync();
            return homework;
        }

        public async Task<Homework> GetAsync(int? id)
        {
            var homework = await _db.Homeworks.FindAsync(id);
            return homework;
        }

        public async Task CreateAsync(Homework homework)
        {
            await _db.Homeworks.AddAsync(homework);
            await _db.SaveChangesAsync();
        }

        public void Update(Homework homework)
        {
            _db.Entry(homework).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Homework> Find(Func<Homework, bool> predicate)
        {
            var homework = _db.Homeworks
                .Where(predicate)
                .ToList();
            return homework;
        }

        public void Delete(Homework homework)
        {
            _db.Homeworks.Remove(homework);
            _db.SaveChanges();
        }
    }
}
