using Audree.Structure.Application.DTO.Master;
using Audree.Structure.Core.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi.Core.DTO;

namespace Audree.Structure.Api.Helpers
{
    public class AutoMapperClass: Profile
    {
        //public AutoMapperClass()
        //{
        //    CreateMap<Usernew,UserDTO>().ReverseMap();
        //}

        public AutoMapperClass()
        {
            CreateMap<Usernew, UserNewDTO>().ReverseMap();
            CreateMap<employee, EmployeeDTO>().ReverseMap();
        }
    }
}
