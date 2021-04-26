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
    public class LecturerController : ControllerBase
    {
        private readonly IDTOService<LecturerDTO, Lecturer> _db;
        private readonly IMapper _mapper;

        public LecturerController(IDTOService<LecturerDTO, Lecturer> lecturerService, IWebMapper mapper)
        {
            _db = lecturerService;
            _mapper = mapper.Create();
        }

        ///<summary>
        ///GET: Lecturer
        ///</summary>
        [HttpGet]
        public async Task<IEnumerable<LecturerDTO>> Get()
        {
            return await _db.GetAllAsync();
        }

        ///<summary>
        ///GET: Lecturer by id
        ///</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<LecturerDTO>> Get(int? id)
        {
            if (id == null)
                return BadRequest();

            var prof = await _db.GetAsync(id);
            return Ok(prof);
        }

        ///<summary>
        ///POST: Lecturer
        ///</summary>
        [HttpPost]
        public async Task<ActionResult<LecturerDTO>> Post(LecturerViewModel profViewModel)
        {
            if (profViewModel == null)
                return BadRequest();

            var prof = _mapper.Map<LecturerDTO>(profViewModel);
            await _db.CreateAsync(prof);
            return Ok(profViewModel);
        }

        ///<summary>
        ///PUT: Lecturer
        ///</summary>
        [HttpPut]
        public async Task<ActionResult<LecturerDTO>> Put(LecturerViewModel profViewModel)
        {
            if (profViewModel == null)
                return BadRequest();

            if (!_db.Find(p => p.Id == profViewModel.Id).Any())
                return NotFound();

            var prof = _mapper.Map<LecturerDTO>(profViewModel);
            await _db.UpdateAsync(prof);
            return Ok(profViewModel);
        }

        ///<summary>
        ///DELETE: Lecturer by id
        ///</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<LecturerDTO>> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var prof = await _db.GetAsync(id);
            await _db.DeleteAsync(id);
            return Ok(prof);
        }
    }
}
