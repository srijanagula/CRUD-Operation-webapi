using System;
using System.Collections.Generic;
using System.Text;

namespace Audree.Structure.Application.DTO.Master
{
   public class UserNewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }
}
