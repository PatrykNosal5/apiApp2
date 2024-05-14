using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{

    public interface IHospitalService
    {

        Task<IEnumerable<Doctor>> GetDoctor();
        Task<Boolean> DeleteDoctor(int id);
        Task<Boolean> PostDoctor(DoctorToAdd doctor);
        Task<Boolean> PutDoctor(DoctorToAdd doctor);
        Task<PrescriptionToShow> GetPrescription(int id);
        Task<Boolean> Register(RegistrationRequest regreq);
        Task<string> Check(string token);
        Task<string> Login(LoginRequest logreq);



        public class HospitalService : IHospitalService
        {
            private readonly HospitalContext _context;
            private readonly IConfiguration _config;
            public HospitalService(HospitalContext context, IConfiguration config)
            {
                _context = context;
                _config = config;
            }

            public async Task<string> Login(LoginRequest logreq) 
            {

                var user = _context.Users.Where(u => u.Login ==  logreq.Login).FirstOrDefault();

                string passwordHash = user.Password;



                //var userx = _context.Users.FirstOrDefault(l => l.RefreshToken == user.RefreshToken);


                var secret = _config["JWT:Secret"];
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


                var refreshToken = new JwtSecurityToken(            //sprawdzamy czy istnieje taki refresh token jesli tak to wysylamy na koncowke refreesh i dajemy koncowke ze zwroceniem nowego refresh(jwt?) tokenu   // nie trzeba claimsow same refreshe itp
                      issuer: _config["JWT:Issuer"],
                      audience: _config["JWT:Audience"],
                      expires: DateTime.Now.AddMinutes(3),
                      signingCredentials: creds
                      );



                //string currHashedPassword = hasher.HashPassword(user, logreq.Password);//hashowanie hasla gotowe

                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: logreq.Password,
                        salt: Encoding.UTF8.GetBytes(user.Salt),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8
                        ));

                if (hashed.Equals(passwordHash)) {
                    var newToken = new JwtSecurityTokenHandler().WriteToken(refreshToken);
                    user.RefreshToken = newToken;
                    await _context.SaveChangesAsync();
                    return newToken;
                }


                // walidacjia hasla i dehashowanie go
               

                return null;
            }

            public async Task<string> Check(string token) 
            {
                var userx = _context.Users.FirstOrDefault(l => l.RefreshToken == token);
                var secret = _config["JWT:Secret"];
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


                if (userx == null) {
                    return null;
                }

                var refreshToken = new JwtSecurityToken(            //sprawdzamy czy istnieje taki refresh token jesli tak to wysylamy na koncowke refreesh i dajemy koncowke ze zwroceniem nowego refresh(jwt?) tokenu   // nie trzeba claimsow same refreshe itp
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
                );

                var newToken = new JwtSecurityTokenHandler().WriteToken(refreshToken);

                /*var JWTtoken = new JwtSecurityToken(            //sprawdzamy czy istnieje taki refresh token jesli tak to wysylamy na koncowke refreesh i dajemy koncowke ze zwroceniem nowego refresh(jwt?) tokenu   // nie trzeba claimsow same refreshe itp
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
                );*/


                userx.RefreshToken = newToken;
                await _context.SaveChangesAsync();


                return newToken;
            }

            public async Task<Boolean> Register(RegistrationRequest regreq)
            {
                var userx = _context.Users.FirstOrDefault(l => l.Login == regreq.Login);
                if (userx == null)
                {
                    var user = new User
                    {
                        Login = regreq.Login
                    };

                    
                    
                    
                    byte[] salt = new byte[128 / 8];
                    using (var rng = RandomNumberGenerator.Create()) 
                    {
                        rng.GetBytes(salt);
                    }
                    user.Salt = Convert.ToBase64String(salt);// czy to okej?
                    

                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: regreq.Password,
                        salt: salt,
                        prf:KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8
                        ));

                    user.Password = hashed;
                    await _context.AddAsync(user);
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    Console.WriteLine("User exists.");
                    return false;
                }
            }

            public async Task<IEnumerable<Doctor>> GetDoctors()
            {
                return await _context.Doctors
                    .ToListAsync();
            }

            public async Task<IEnumerable<Doctor>> GetDoctor()
            {
                return await _context.Doctors.Select(e => new Doctor
                {
                    IdDoctor = e.IdDoctor,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email
                }
                                                    ).ToListAsync();
                //.Where(e => e.IdDoctor == id)
                // return await _context.Doctors.Select(e => e.IdDoctor == id);
            }



            public async Task<Boolean> DeleteDoctor(int id)
            {
                //return await _context.Doctors.ExecuteDeleteAsync(e => e.IdDoctor == id);


                var lekarz = _context.Doctors.FirstOrDefault(l => l.IdDoctor == id);

                if (lekarz != null)
                {
                    _context.Doctors.Remove(lekarz);
                    _context.SaveChanges();
                    Console.WriteLine("Doctor deleted successfully.");
                    return true;
                }
                return false;

                //return _context.Doctors.Remove(lekarz).ToListAsync();
                // return await _context.Doctors.Select(e => e.IdDoctor == id);
            }
            public async Task<Boolean> PostDoctor(DoctorToAdd doctor)
            {

                if (doctor != null)
                {
                    var newDoctor = new Doctor
                    {
                        IdDoctor = doctor.IdDoctor,
                        FirstName = doctor.FirstName,
                        LastName = doctor.LastName,
                        Email = doctor.Email,
                    };
                    _context.Doctors.Add(newDoctor);
                    _context.SaveChanges();
                    Console.WriteLine("Doctor Added successfully.");
                    return true;
                }
                return false;

                //return _context.Doctors.Remove(lekarz).ToListAsync();
                // return await _context.Doctors.Select(e => e.IdDoctor == id);
            }
            public async Task<Boolean> PutDoctor(DoctorToAdd doctor)
            {
                var lekarz = _context.Doctors.FirstOrDefault(l => l.IdDoctor == doctor.IdDoctor);

                if (doctor != null && lekarz != null)
                {
                    var newDoctor = new Doctor
                    {
                        IdDoctor = doctor.IdDoctor,
                        FirstName = doctor.FirstName,
                        LastName = doctor.LastName,
                        Email = doctor.Email,
                    };


                    lekarz.FirstName = newDoctor.FirstName;
                    lekarz.LastName = newDoctor.LastName;
                    lekarz.Email = newDoctor.Email;
                    lekarz.IdDoctor = newDoctor.IdDoctor;



                    //_context.Doctors.Update(lekarz);
                    _context.SaveChanges();
                    Console.WriteLine("Doctor edited successfully.");
                    return true;
                }
                return false;

                //return _context.Doctors.Remove(lekarz).ToListAsync();
                // return await _context.Doctors.Select(e => e.IdDoctor == id);
            }


            public async Task<PrescriptionToShow> GetPrescription(int id)
            {
                var prescription = _context.Prescriptions.Where(m => m.IdPrescription == id).FirstOrDefault();
                var meds = _context.PrescriptionMedicaments.Where(m => m.Prescription.IdPrescription == id).ToList();
                // z meds wycaignac id i potem tych id szukac w medicament i stamtad robic nowe medicanament int prescription

                var docc = _context.Doctors.Where(m => m.IdDoctor == prescription.IdDoctor).FirstOrDefault();
                var patt = _context.Patients.Where(m => m.IdPatient == prescription.IdPatient).FirstOrDefault();

                var doc = new DoctorInPrescription
                {
                    FirstName = docc.FirstName,
                    LastName = docc.LastName
                };

                var patient = new PatientInPrescription
                {
                    FirstName = patt.FirstName,
                    LastName = patt.LastName
                };

                var medList = new List<MedicamentInPrescription>();

                for (int i = 0; i < meds.Count; i++)
                {
                    var med = meds[i].IdMedicament;
                    var medicament = _context.Medicaments.FirstOrDefault(l => l.IdMedicament == med);
                    var newMed = new MedicamentInPrescription
                    {
                        Name = medicament.Name
                    };
                    medList.Add(newMed);
                }
                var prescToShow = new PrescriptionToShow
                {
                    doctor = doc,
                    patient = patient,
                    Medicaments = medList

                };

                /*for (int i = 0; i<meds.Count; i++)
                {
                    var med = meds[i].IdMedicament;
                    var medicament = _context.Medicaments.FirstOrDefault(l => l.IdMedicament == med);
                    var newMed = new MedicamentInPrescription
                    {
                        Name = medicament.Name
                    };
                    prescToShow.Medicaments.Add(newMed);
                }*/

                return prescToShow;
            }
        }
    }
}
