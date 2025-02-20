using Demo_SQL.DTO;
using Demo_SQL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Demo_SQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTableController : ControllerBase
    {
        private readonly TestsqlContext TestsqlContext;

        public UserTableController(TestsqlContext TestsqlContext)
        {
            this.TestsqlContext = TestsqlContext;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<UserTableDTO>>> Get()
        {
            var List = await TestsqlContext.UserTables.Select(
                s => new UserTableDTO
                {
                    Id = s.Id,
                    Username = s.Username,
                    Password = s.Password,
                    CreateTime = s.CreateTime
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<UserTableDTO>> GetUserById(int Id)
        {
            var UserTable = await TestsqlContext.UserTables.Select(s => new UserTableDTO
            {
                Id = s.Id, 
                Username = s.Username,
                Password = s.Password,
                CreateTime = s.CreateTime
            }).FirstOrDefaultAsync(s => s.Id == Id);
            if (UserTable == null)
            {
                return NotFound();
            }
            else
            {
                return UserTable;
            }
        }

        [HttpPost("InsertUser")]
        public async Task<HttpStatusCode> InsertUser(UserTableDTO User)
        {
            var entity = new UserTable()
            { 
                Username = User.Username,
                Password = User.Password,
                CreateTime = User.CreateTime
            };
            TestsqlContext.UserTables.Add(entity);
            await TestsqlContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateUser")]
        public async Task<HttpStatusCode> UpdateUser(UserTableDTO User)
        {
            var entity = await TestsqlContext.UserTables.FirstOrDefaultAsync(s => s.Id == User.Id);
            entity.Username = User.Username;
            entity.Password = User.Password;
            entity.CreateTime = User.CreateTime;
            await TestsqlContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteUser/{Id}")]
        public async Task<HttpStatusCode> DeleteUser(int Id)
        {
            var entity = new UserTable()
            {
                Id = Id
            };
            TestsqlContext.UserTables.Attach(entity);
            TestsqlContext.UserTables.Remove(entity);
            await TestsqlContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
