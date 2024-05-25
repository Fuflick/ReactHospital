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
    public class DocDiagnosesController : ControllerBase
    {
        private MyDbContext _dbContext;

        public DocDiagnosesController()
        {
            _dbContext = new MyDbContext();
        }

        [HttpGet]
        public async Task GetAll()
        {
            await Response.WriteAsJsonAsync(_dbContext.DocDiagnoses.ToListAsync());
        }
        
        [HttpGet("id")]
        public async Task Get(int id)
        {
            try
            {
                var doctor = await _dbContext.DocDiagnoses.FirstOrDefaultAsync(x => x.Id == id);
                if (doctor == null)
                {
                    Response.StatusCode = 404;
                    await Response.WriteAsJsonAsync(new { message = "Not Found" });
                }
                else
                {
                    await Response.WriteAsJsonAsync(doctor);
                }
            }
            catch (Exception ex)
            {
                await Response.WriteAsJsonAsync(ex.Message);
            }
        }

        [HttpPost]
        public async Task Create(DocDiagnose docDiagnose)
        {
            try
            {
                var docDiag = await _dbContext.Doctor.FirstOrDefaultAsync(x => x.Id == docDiagnose.Id);
                if (docDiag == null)
                {
                    var doc = await _dbContext.Doctor.FirstOrDefaultAsync(x => x.Id == docDiagnose.DocId);
                    var diag = await _dbContext.Diagnose.FirstOrDefaultAsync(x => x.Id == docDiagnose.DiagId);
                    if (doc != null && diag != null)
                    {
                        await _dbContext.DocDiagnoses.AddAsync(docDiagnose);
                        await _dbContext.SaveChangesAsync();
                        await Response.WriteAsJsonAsync(docDiagnose);
                    }
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
        public async Task Edit(DocDiagnose docDiagnose)
        {
            try
            {
                var docDiag = await _dbContext.DocDiagnoses.FirstOrDefaultAsync(x => x.Id == docDiagnose.Id);
                if (docDiag == null)
                {
                    Response.StatusCode = 404;
                    await Response.WriteAsJsonAsync(new {message = "Not Found"});
                }

                docDiag.DiagId = docDiagnose.DiagId;
                docDiag.DocId = docDiagnose.DocId;
                _dbContext.Update(docDiag);
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
                var docDiagnose = await _dbContext.DocDiagnoses.FindAsync(id);
                if (docDiagnose != null)
                {
                    _dbContext.DocDiagnoses.Remove(docDiagnose);
                    await _dbContext.SaveChangesAsync();
                    await Response.WriteAsJsonAsync(docDiagnose);
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
