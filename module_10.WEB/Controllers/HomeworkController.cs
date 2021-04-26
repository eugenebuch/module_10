using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces.ServiceInterfaces;
using module_10.BLL.Services;
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
    public class HomeworkController : ControllerBase
    {
        private readonly IDTOService<HomeworkDTO, Homework> _db;
        private readonly IMapper _mapper;

        public HomeworkController(IDTOService<HomeworkDTO, Homework> homeworkService, IWEB_Mapper mapper)
        {
            _db = homeworkService;
            _mapper = mapper.Create();
        }

        ///<summary>
        ///GET: Homework
        ///</summary>
        [HttpGet]
        public async Task<IEnumerable<HomeworkDTO>> Get()
        {
            return await _db.GetAllAsync();
        }

        ///<summary>
        ///GET: Homework by id
        ///</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<HomeworkDTO>> Get(int? id)
        {
            if (id == null)
                return BadRequest();

            var homework = await _db.GetAsync(id);
            return Ok(homework);
        }

        ///<summary>
        ///POST: Homework
        ///</summary>
        [HttpPost]
        public async Task<ActionResult<HomeworkDTO>> Post(HomeworkViewModel homeworkViewModel)
        {
            if (homeworkViewModel == null)
                return BadRequest();

            var homework = _mapper.Map<HomeworkDTO>(homeworkViewModel);
            await _db.CreateAsync(homework);
            return Ok(homeworkViewModel);
        }

        ///<summary>
        ///PUT: Homework
        ///</summary>
        [HttpPut]
        public async Task<ActionResult<HomeworkDTO>> Put(HomeworkViewModel homeworkViewModel)
        {
            if (homeworkViewModel == null)
                return BadRequest();

            var homework = _mapper.Map<HomeworkDTO>(homeworkViewModel);
            if (!_db.Find(h => h.Id == homework.Id).Any())
                return NotFound();

            await _db.UpdateAsync(homework);
            return Ok(homeworkViewModel);
        }

        ///<summary>
        ///DELETE: Homework by id
        ///</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<HomeworkDTO>> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var homework = await _db.GetAsync(id);
            await _db.DeleteAsync(id);
            return Ok(homework);
        }
    }
}
