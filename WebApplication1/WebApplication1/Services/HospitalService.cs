using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
   
        public interface IHospitalService
        {
            Task<IEnumerable<Doctor>> GetDoctor(int id);
        }
        public class HospitalService : IHospitalService
        {
            private readonly HospitalContext _context;
            public HospitalService(HospitalContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Doctor>> GetDoctors()
            {
                return await _context.Doctors
                    .ToListAsync();
        }

            public async Task<IEnumerable<Doctor>> GetDoctor(int id)
            {
                return await _context.Doctors.Select(e => new Doctor//projekcja i select sam wybierze, jesli select potem, to jest to nie mozliwe i musza byc includy, musi btyc select najpierw,
                {
                    IdDoctor = e.IdDoctor,
                    FirstName = e.FirstName,
                    LastName = e.LastName, 
                    Email = e.Email
                }
                ).Where(e => e.IdDoctor == id).ToListAsync();
            }
        }
    
}
