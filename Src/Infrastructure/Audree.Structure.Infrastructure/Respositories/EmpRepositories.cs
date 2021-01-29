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
    public class EmpRepositories: IRespositoriesEmp
    {
       private readonly IUnitOfWork _unitOfWork;
       private  readonly Context _dbw;

        public EmpRepositories(IUnitOfWork unitOfWork, Context dbw)
        {
            _unitOfWork = unitOfWork;
            _dbw = dbw;
        }

        public async Task<List<employee>> Getemp()
        {
            try
            {
                using (_unitOfWork)
                {
                    var result = await _dbw.Emp.OrderBy(x => x.EmpId).ToListAsync();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> PostUserOrUpdateEmp(employee employee1)
        {
            string Message = "";
            using (_unitOfWork)
            {
                using (var transaction = _dbw.Database.BeginTransaction())
                {
                    try
                    {
                        var userId = employee1.EmpId;
                        //var userId = 18;
                        if (userId == 0)
                        {

                            employee1.Name = employee1.Name;
                            employee1.Email = employee1.Email;
                            employee1.Phone = employee1.Phone;
                            await _dbw.Emp.AddAsync(employee1);

                            //await _dbw.usernew.AddAsync(usernew);
                            _dbw.SaveChanges();
                            Message = "created successfully";
                            //auditTraildata.ExecuteAudit("Role", role.Id, role.CreatedById, role.LoginUSerPlantId, role.RoleName, null, 0, (int)EnumDBActions.Create, "Role Name", role.Comments, role.LoginUSerPlant, role.LoginUSerName, _batchContext);
                        }
                        else
                        {
                            var roleExist = await _dbw.Emp.AsNoTracking().Where(w => w.EmpId == employee1.EmpId).FirstOrDefaultAsync();
                            if (roleExist != null)
                            {
                                employee1.Name = employee1.Name;
                                employee1.Email = employee1.Email;
                                employee1.Phone = employee1.Phone;
                            }
                            _dbw.Emp.Update(roleExist);
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

        public async Task<string> DeleteEmp(employee employee)
        {
            string Message = "";
            employee emp = await _dbw.Emp.Where(a => a.EmpId == employee.EmpId).FirstOrDefaultAsync();
            _dbw.Emp.Remove(emp); await _dbw.SaveChangesAsync();
            Message = "deleted successfully";
            return Message;
        }
     


        public async Task<employee> GetEmpById(int? id)
        {
            using (_unitOfWork)
            {
                return await _dbw.Emp.FirstOrDefaultAsync(x => x.EmpId == id);
            }
        }

        public async Task<employee> Update(employee employee)
        {
            _dbw.Entry(employee).State = EntityState.Modified;
            await _dbw.SaveChangesAsync();
            return employee;
        }
    }
}
