using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Audree.Structure.Core.Models;
using Audree.Structure.Infrastructure.Data;
using Audree.Structure.Infrastructure.Respositories;
using AutoMapper;
using Tweetinvi.Core.DTO;
using Audree.Structure.Core.Contracts.IRespositories;
using Audree.Structure.Application.DTO.Master;

namespace Audree.Structure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsernewsController : ControllerBase
    {
        //private readonly IMapper mapper;
        private UsernewIRespositories _userNewRepositories;
        private IMapper _mapper;
      
        public UsernewsController(UsernewIRespositories userNewRepositories, IMapper mapper)
        {
            _userNewRepositories = userNewRepositories;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userNewRepositories.GetUser());
        }

        [HttpPost("PostUserOrUpdateUser")]
        public async Task<IActionResult> PostUserOrUpdateUser(UserNewDTO userDTO)
        {
            
            return Ok(await _userNewRepositories.PostUserOrUpdateUser(_mapper.Map<Usernew>(userDTO)));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(UserNewDTO userDTO)
        {
            return Ok(await _userNewRepositories.Delete(_mapper.Map<Usernew>(userDTO)));
        }
       
        
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var RoleName = await _userNewRepositories.GetUserById(id);
            var RoleData = _mapper.Map<Usernew>(RoleName);
            if (RoleName == null)
            {
                return NotFound();
            }
            return Ok(RoleData);
        }
        [HttpPut]
        public async Task<IActionResult> Put(int id, UserNewDTO userDTO)
        {

            return Ok(await _userNewRepositories.Update(_mapper.Map<Usernew>(userDTO)));

        }
    }
}

    
       
