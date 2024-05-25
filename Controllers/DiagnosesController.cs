using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactHospital.Models;

namespace ReactHospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosesController : ControllerBase
    {
        private MyDbContext _dbContext;

        public DiagnosesController()
        {
            _dbContext = new MyDbContext();
        }

        [HttpGet]
        public async Task GetAll()
        {
            await Response.WriteAsJsonAsync(_dbContext.Diagnose.ToListAsync());
        }
        
        [HttpGet("id")]
        public async Task Get(int id)
        {
            try
            {
                var diagnose = await _dbContext.Diagnose.FirstOrDefaultAsync(x => x.Id == id);
                if (diagnose == null)
                {
                    Response.StatusCode = 404;
                    await Response.WriteAsJsonAsync(new { message = "Not Found" });
                }
                else
                {
                    await Response.WriteAsJsonAsync(diagnose);
                }
            }
            catch (Exception ex)
            {
                await Response.WriteAsJsonAsync(ex.Message);
            }
        }

        [HttpPost]
        public async Task Create(Diagnose? diagnose)
        {
            try
            {
                var diag = await _dbContext.Diagnose.FirstOrDefaultAsync(x => x.Id == diagnose.Id);
                if (diag == null)
                {
                    await _dbContext.Diagnose.AddAsync(diagnose);
                    await _dbContext.SaveChangesAsync();
                    await Response.WriteAsJsonAsync(diagnose);
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
        public async Task Edit(Diagnose diagnose)
        {
            try
            {
                var diag = await _dbContext.Diagnose.FirstOrDefaultAsync(x => x.Id == diagnose.Id);
                if (diag == null)
                {
                    Response.StatusCode = 404;
                    await Response.WriteAsJsonAsync(new {message = "Not Found"});
                }

                diag.Body = diagnose.Body;
                diag.Date = diagnose.Date;
                _dbContext.Update(diag);
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
                var diagnose = await _dbContext.Diagnose.FindAsync(id);
                if (diagnose != null)
                {
                    _dbContext.Diagnose.Remove(diagnose);
                    await _dbContext.SaveChangesAsync();
                    await Response.WriteAsJsonAsync(diagnose);
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
