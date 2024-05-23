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
    public class PatientsController : ControllerBase
    {
        private MyDbContext _dbContext;

        public PatientsController()
        {
            _dbContext = new MyDbContext();
        }

        [HttpGet]
        public async Task GetAll()
        {
            await Response.WriteAsJsonAsync(_dbContext.Patient.ToListAsync());
        }
        
        [HttpGet("id")]
        public async Task Get(int id)
        {
            try
            {
                var patient = await _dbContext.Patient.FirstOrDefaultAsync(x => x.Id == id);
                if (patient == null)
                {
                    Response.StatusCode = 404;
                    await Response.WriteAsJsonAsync(new { message = "Not Found" });
                }
                else
                {
                    await Response.WriteAsJsonAsync(patient);
                }
            }
            catch (Exception ex)
            {
                await Response.WriteAsJsonAsync(ex.Message);
            }
        }

        [HttpPost]
        public async Task Create(Patient? patient)
        {
            try
            {
                var pat = await _dbContext.Patient.FirstOrDefaultAsync(x => x.Id == patient.Id);
                if (pat == null)
                {
                    await _dbContext.Patient.AddAsync(patient);
                    await _dbContext.SaveChangesAsync();
                    await Response.WriteAsJsonAsync(patient);
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
        public async Task Edit(Patient patient)
        {
            try
            {
                var pat = await _dbContext.Patient.FirstOrDefaultAsync(x => x.Id == patient.Id);
                if (pat == null)
                {
                    Response.StatusCode = 404;
                    await Response.WriteAsJsonAsync(new {message = "Not Found"});
                }

                pat = patient;
                _dbContext.Update(pat);
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
                var patient = await _dbContext.Patient.FindAsync(id);
                if (patient != null)
                {
                    _dbContext.Patient.Remove(patient);
                    await _dbContext.SaveChangesAsync();
                    await Response.WriteAsJsonAsync(patient);
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
