using Audree.Structure.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Audree.Structure.Core.Contracts.IRespositories
{
    public  interface IRespositoriesEmp
    {
        Task<List<employee>> Getemp();
        Task<employee> GetEmpById(int? id);

        Task<employee> Update(employee employee);
        Task<string> PostUserOrUpdateEmp(employee employee);
        Task<string> DeleteEmp(employee employee);
    }
}
