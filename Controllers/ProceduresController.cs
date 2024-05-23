using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactHospital.Models;

namespace ReactHospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProceduresController : ControllerBase
    {
        private MyDbContext _dbContext;

        public ProceduresController()
        {
            _dbContext = new MyDbContext();
        }

        [HttpGet]
        public async Task GetAll()
        {
            await Response.WriteAsJsonAsync(_dbContext.Procedure.ToListAsync());
        }
        
        [HttpGet("id")]
        public async Task Get(int id)
        {
            try
            {
                var procedure = await _dbContext.Procedure.FirstOrDefaultAsync(x => x.Id == id);
                if (procedure == null)
                {
                    Response.StatusCode = 404;
                    await Response.WriteAsJsonAsync(new { message = "Not Found" });
                }
                else
                {
                    await Response.WriteAsJsonAsync(procedure);
                }
            }
            catch (Exception ex)
            {
                await Response.WriteAsJsonAsync(ex.Message);
            }
        }

        [HttpPost]
        public async Task Create(Procedure? procedure)
        {
            try
            {
                var proc = await _dbContext.Procedure.FirstOrDefaultAsync(x => x.Id == procedure.Id);
                if (proc == null)
                {
                    await _dbContext.Procedure.AddAsync(procedure);
                    await _dbContext.SaveChangesAsync();
                    await Response.WriteAsJsonAsync(procedure);
                }
                else
                {
                    Response.StatusCode = 400;
                    await Response.WriteAsJsonAsync(new { message = "Already exist" });
                }
            }
            catch (Exception exception)
            {
                await Response.WriteAsJsonAsync(exception.Message);
            }
        }

        [HttpPatch]
        public async Task Edit(Procedure procedure)
        {
            try
            {
                var proc = await _dbContext.Procedure.FirstOrDefaultAsync(x => x.Id == procedure.Id);
                if (proc == null)
                {
                    Response.StatusCode = 404;
                    await Response.WriteAsJsonAsync(new {message = "Not Found"});
                }

                proc = procedure;
                _dbContext.Update(proc);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Response.WriteAsJsonAsync(ex.Message);
            }
        }

        [HttpDelete("id")]
        public async Task Delete(int id)
        {
            try
            {
                var procedure = await _dbContext.Procedure.FindAsync(id);
                if (procedure != null)
                {
                    _dbContext.Procedure.Remove(procedure);
                    await _dbContext.SaveChangesAsync();
                    await Response.WriteAsJsonAsync(procedure);
                }
                else
                {
                    Response.StatusCode = 404;
                    await Response.WriteAsJsonAsync(new { message = "Not Found" });
                }
            }
            catch (Exception ex)
            {
                await Response.WriteAsJsonAsync(ex);
            }
            

        }
    }
}
