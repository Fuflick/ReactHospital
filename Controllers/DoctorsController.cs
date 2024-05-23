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
    public class DoctorsController : ControllerBase
    {
        private MyDbContext _dbContext;

        public DoctorsController()
        {
            _dbContext = new MyDbContext();
        }

        [HttpGet]
        public async Task GetAll()
        {
            await Response.WriteAsJsonAsync(_dbContext.Doctor.ToListAsync());
        }
        
        [HttpGet("id")]
        public async Task Get(int id)
        {
            try
            {
                var doctor = await _dbContext.Doctor.FirstOrDefaultAsync(x => x.Id == id);
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
        public async Task Create(Doctor? doctor)
        {
            try
            {
                var diag = await _dbContext.Doctor.FirstOrDefaultAsync(x => x.Id == doctor.Id);
                if (diag == null)
                {
                    await _dbContext.Doctor.AddAsync(doctor);
                    await _dbContext.SaveChangesAsync();
                    await Response.WriteAsJsonAsync(doctor);
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
        public async Task Edit(Doctor doctor)
        {
            try
            {
                var doc = await _dbContext.Doctor.FirstOrDefaultAsync(x => x.Id == doctor.Id);
                if (doc == null)
                {
                    Response.StatusCode = 404;
                    await Response.WriteAsJsonAsync(new {message = "Not Found"});
                }

                doc = doctor;
                _dbContext.Update(doc);
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
                var doctor = await _dbContext.Doctor.FindAsync(id);
                if (doctor != null)
                {
                    _dbContext.Doctor.Remove(doctor);
                    await _dbContext.SaveChangesAsync();
                    await Response.WriteAsJsonAsync(doctor);
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
