using AutoMapper;
using Microsoft.Extensions.Logging;
using module_10.BLL.DTO;
using module_10.BLL.Infrastructure;
using module_10.BLL.Interfaces;
using module_10.BLL.Interfaces.ServiceInterfaces;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace module_10.BLL.Services
{
    public class StudentService : IDTOService<StudentDTO, Student>
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public StudentService(IRepository<Student> repository, IBLL_Mapper mapper, ILoggerFactory factory = null)
        {
            _studentRepository = repository;
            _logger = factory?.CreateLogger("Student Service");
            _mapper = mapper.CreateMapper();
        }

        public async Task<IEnumerable<StudentDTO>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            if (!students.Any())
            {
                var mes = "There is no Students in data base";
                _logger?.LogWarning(mes);
                throw new ValidationException(mes);
            }

            return students
                .Select(s => _mapper.Map<StudentDTO>(s));
        }

        public async Task<StudentDTO> GetAsync(int? id)
        {
            var validator = new Validations();
            validator.IdValidation(id, _logger);

            var student = await _studentRepository.GetAsync(id);

            validator.EntityValidation(student, _logger, nameof(student));

            return _mapper.Map<StudentDTO>(student);
        }

        public async Task CreateAsync(StudentDTO item)
        {
            var student = _mapper.Map<Student>(item);
            await _studentRepository.CreateAsync(student);
        }

        public async Task UpdateAsync(StudentDTO item)
        {
            var student = await _studentRepository.GetAsync(item.Id);

            var validator = new Validations();
            validator.EntityValidation(student, _logger, nameof(student));

            student.FirstName = item.FirstName;
            student.LastName = item.LastName;
            _studentRepository.Update(student);
        }

        public IEnumerable<StudentDTO> Find(Func<Student, bool> predicate)
        {
            var students = _studentRepository
                .Find(predicate)
                .ToList();
            return students
                .Select(s => _mapper.Map<StudentDTO>(s));
        }

        public async Task DeleteAsync(int? id)
        {
            var validator = new Validations();
            validator.IdValidation(id, _logger);

            var student = await _studentRepository.GetAsync(id);
            validator.EntityValidation(student, _logger, nameof(student));

            _studentRepository.Delete(student);
        }
    }
}
