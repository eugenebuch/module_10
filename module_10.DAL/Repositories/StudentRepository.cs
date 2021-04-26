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
    public class StudentRepository : IRepository<Student>
    {
        private readonly DataContext _db;

        public StudentRepository(DataContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var Student = await _db.Students.ToListAsync();
            return Student;
        }

        public async Task<Student> GetAsync(int? id)
        {
            var Student = await _db.Students.FindAsync(id);
            return Student;
        }

        public async Task CreateAsync(Student Student)
        {
            _db.Students.AddAsync(Student);
            await _db.SaveChangesAsync();
        }

        public void Update(Student Student)
        {
            _db.Entry(Student).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Student> Find(Func<Student, bool> predicate)
        {
            var Student = _db.Students
                .Where(predicate)
                .ToList();
            return Student;
        }

        public void Delete(Student Student)
        {
            _db.Students.Remove(Student);
            _db.SaveChanges();
        }
    }
}
