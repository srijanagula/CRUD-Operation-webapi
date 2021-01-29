using Audree.Structure.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Audree.Structure.Core.Contracts.IRespositories
{
   public interface UsernewIRespositories
    {
       
        Task<List<Usernew>> GetUser();
        Task<Usernew>GetUserById(int? Id);
        Task<Usernew> Update(Usernew usernew);
        Task<string> PostUserOrUpdateUser(Usernew usernew);
        Task<string> Delete(Usernew usernew);

    }
}
