using XO.Entities.ModelExtensions;
using XO.Entities.Models;
using XO.Repository.Interfaces.Admin;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using XO.Common.Enums;
using XO.Common.Dtos.AdminAccount;

namespace XO.Repository.Admin
{
    public class AdminGroupPermissionRepository : BaseRepository<AdminGroupPermission>, IAdminGroupPermissionRepository
    {
        public AdminGroupPermissionRepository(XOContext context)
            : base(context)
        {
        }

        public async Task<List<AdminGroupPermission_View00>> GetPermissionByGroupAndModule(int groupId, 
            string module)
        {
            var dbResults = new List<AdminGroupPermission_View00>();
            var groupIdP = new SqlParameter("groupId", groupId);
            var moduleP = new SqlParameter("module", module);

            dbResults = await _context.AdminGroupPermission_View00.FromSqlRaw(";EXEC GetPermissionByGroupAndModule @groupId, @module"
                        , groupIdP, moduleP).ToListAsync();

            return dbResults;
        }
        public async Task<int> InsertOrUpdatePermission(int groupId, int permissionId, int status)
        {
            var dbResults = int.MinValue;
            
            var curDate = DateTime.Now;

            try
            {
                dbResults = await _context.Database.ExecuteSqlRawAsync("InsertOrUpdatePermission @p0, @p1, @p2, @p3"
                    , parameters: new object[] { permissionId, groupId, status, curDate });
            }
            catch (Exception ex)
            {
                dbResults = 0;
            }

            return dbResults;
        }
        public async Task<string[]> GetPermissionByGroup(int groupId)
        {
            var result = Task.Run(() => new string[0]);
            result = (from groupPermission in _context.AdminGroupPermission
                      where groupPermission.Status == (int)EStatus.Using &&
                      groupPermission.GroupId == groupId
                      join permission in _context.AdminPermission on groupPermission.PermissionId equals permission.Id
                      select new GroupPermissionDto
                      {
                          Id = groupPermission.Id,
                          GroupId = groupPermission.GroupId,
                          PermissionId = groupPermission.PermissionId,
                          PermissionIdName = permission.Name,
                          Status = groupPermission.Status.GetValueOrDefault()
                      })
                      .Select(c => c.PermissionIdName)
                      .ToArrayAsync();

            await Task.WhenAll(result);
            return await result;
        }
    }
}
