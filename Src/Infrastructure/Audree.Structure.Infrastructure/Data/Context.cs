using Audree.Structure.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audree.Structure.Infrastructure.Data
{
    public class Context :DbContext 
    {
        public Context(DbContextOptions<Context> options)
           : base(options)
        {

        }

        public DbSet<Usernew> Usernew { get; set; }
        public DbSet<employee> Emp { get; set; }


    }
}
