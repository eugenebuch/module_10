using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces.ServiceInterfaces;
using module_10.DAL.Entities;
using module_10.WEB.Interfaces;
using module_10.WEB.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace module_10.WEB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IDTOService<StudentDTO, Student> _db;
        private readonly IMapper _mapper;

        public StudentController(IDTOService<StudentDTO, Student> studentService, IWebMapper mapper)
        {
            _db = studentService;
            _mapper = mapper.Create();
        }

        ///<summary>
        ///GET: Student
        ///</summary>
        [HttpGet]
        public async Task<IEnumerable<StudentDTO>> Get()
        {
            return await _db.GetAllAsync();
        }

        ///<summary>
        ///GET: Student by id
        ///</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> Get(int? id)
        {
            if (id == null)
                return BadRequest();

            var student = await _db.GetAsync(id);
            return Ok(student);
        }

        ///<summary>
        ///POST: Student
        ///</summary>
        [HttpPost]
        public async Task<ActionResult<StudentDTO>> Post(StudentViewModel studentViewModel)
        {
            if (studentViewModel == null)
                return BadRequest();

            var student = _mapper.Map<StudentDTO>(studentViewModel);
            await _db.CreateAsync(student);
            return Ok(studentViewModel);
        }

        ///<summary>
        ///PUT: Student
        ///</summary>
        [HttpPut]
        public async Task<ActionResult<StudentDTO>> Put(StudentViewModel studentViewModel)
        {
            if (studentViewModel == null)
                return BadRequest();

            if (!_db.Find(s => s.Id == studentViewModel.Id).Any())
                return NotFound();

            var student = _mapper.Map<StudentDTO>(studentViewModel);
            await _db.UpdateAsync(student);
            return Ok(studentViewModel);
        }

        ///<summary>
        ///DELETE: Student by id
        ///</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentDTO>> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var student = await _db.GetAsync(id);
            await _db.DeleteAsync(id);
            return Ok(student);
        }
    }
}
