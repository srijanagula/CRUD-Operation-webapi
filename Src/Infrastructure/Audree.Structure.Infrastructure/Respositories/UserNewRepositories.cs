using Audree.Structure.Core.Contracts.IRespositories;
using Audree.Structure.Core.Contracts.IUnitOfWork;
using Audree.Structure.Core.Models;
using Audree.Structure.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audree.Structure.Infrastructure.Respositories
{

    public class UserNewRepositories : UsernewIRespositories
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Context _dbw;

        public UserNewRepositories(IUnitOfWork unitOfWork, Context dbw)
        {
            _unitOfWork = unitOfWork;
            _dbw = dbw;

        }

        public async Task<List<Usernew>> GetUser()
        {
            try
            {
                using(_unitOfWork)
                {
                    var result = await _dbw.Usernew.OrderBy(x => x.Id).ToListAsync();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        public async Task<string> PostUserOrUpdateUser(Usernew usernew)
        {
            string Message = "";
            using (_unitOfWork)
            {
                using (var transaction = _dbw.Database.BeginTransaction())
                {
                    try
                    {
                        var userId = usernew.Id;
                        //var userId = 18;
                        if (userId == 0)
                        {
                            
                            usernew.Name = usernew.Name;
                            usernew.Email = usernew.Email;
                            usernew.Password = usernew.Password;
                            usernew.Phone = usernew.Phone;
                            await _dbw.Usernew.AddAsync(usernew);

                            //await _dbw.usernew.AddAsync(usernew);
                            _dbw.SaveChanges();
                            Message = "created successfully";
                            //auditTraildata.ExecuteAudit("Role", role.Id, role.CreatedById, role.LoginUSerPlantId, role.RoleName, null, 0, (int)EnumDBActions.Create, "Role Name", role.Comments, role.LoginUSerPlant, role.LoginUSerName, _batchContext);
                        }
                        else
                        {
                            var roleExist = await _dbw.Usernew.AsNoTracking().Where(w => w.Id == usernew.Id).FirstOrDefaultAsync();
                            if (roleExist != null)
                            {
                                usernew.Name = usernew.Name;
                                usernew.Email = usernew.Email;
                                usernew.Password = usernew.Password;
                                usernew.Phone = usernew.Phone;
                            }
                            _dbw.Usernew.Update(roleExist);
                            _dbw.SaveChanges();
                            Message = "created successfully";
                        }
                        await transaction.CommitAsync();
                        return Message;
                    }
                    catch (Exception )
                    {
                        Message = "issue";
                        // Message = "TransactionFail";
                        transaction.Rollback();
                        return Message;
                    }
                }
              
            }
        }
        public async Task<string> Delete(Usernew usernew)
        {
            string Message = "";
            Usernew emp = await _dbw.Usernew.Where(a => a.Id == usernew.Id).FirstOrDefaultAsync();
            _dbw.Usernew.Remove(emp); await _dbw.SaveChangesAsync();
            Message = "deleted successfully";
            return Message;
        }
        public async Task<Usernew> GetUserById(int? Id)
        {
            using (_unitOfWork)
            {
                return await _dbw.Usernew.FirstOrDefaultAsync(x => x.Id == Id);
            }
        }
        public async Task<Usernew> Update(Usernew usernew)
        {
            _dbw.Entry(usernew).State = EntityState.Modified;
            await _dbw.SaveChangesAsync();
            return usernew;
        }
    }
}
