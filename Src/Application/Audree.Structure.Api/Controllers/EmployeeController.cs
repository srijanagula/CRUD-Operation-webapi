using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audree.Structure.Core.Contracts.IRespositories;
using Audree.Structure.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Audree.Structure.Application.DTO.Master;
using Audree.Structure.Infrastructure.Data;

namespace Audree.Structure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IRespositoriesEmp _IRespositoriesEmp;
        private IMapper _mapper;
        private readonly Context _dbw;

        public EmployeeController(IRespositoriesEmp iRespositoriesEmp, IMapper mapper,Context dbw)
        {
            _IRespositoriesEmp = iRespositoriesEmp;
            _mapper = mapper;
            _dbw = dbw;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _IRespositoriesEmp.Getemp());
        }

        [HttpPost("PostUserOrUpdateEmp")]
        public async Task<IActionResult> PostUserOrUpdateEmp(EmployeeDTO employeeDTO)
        {

            return Ok(await _IRespositoriesEmp.PostUserOrUpdateEmp(_mapper.Map<employee>(employeeDTO)));
        }
        [HttpPost("DeleteEmp")]
        public async Task<IActionResult> DeleteEmp(EmployeeDTO employeeDTO)
        {
            return Ok(await _IRespositoriesEmp.DeleteEmp(_mapper.Map<employee>(employeeDTO)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var RoleName = await _IRespositoriesEmp.GetEmpById(id);
            var RoleData = _mapper.Map<employee>(RoleName);
            if (RoleName == null)
            {
                return NotFound();
            }
            return Ok(RoleData);
        }
        [HttpPut]
        public async Task<IActionResult> Put(int id, EmployeeDTO employeeDTO)
        {

           return Ok( await _IRespositoriesEmp.Update(_mapper.Map<employee>(employeeDTO)));
          
        }
    }
}

