using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class HospitalContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
        public HospitalContext(DbContextOptions options) : base(options)
        {
        }

        protected HospitalContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>(doc =>
            {
                doc.HasKey(e => e.IdDoctor);

                doc.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                doc.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                doc.Property(e => e.Email).HasMaxLength(100).IsRequired();


            }
            );
            modelBuilder.Entity<Patient>(doc =>
            {
                doc.HasKey(e => e.IdPatient);

                doc.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                doc.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                doc.Property(e => e.Birthdate).IsRequired();

            }
            );
            modelBuilder.Entity<Medicament>(doc =>
            {
                doc.HasKey(e => e.IdMedicament);

                doc.Property(e => e.Name).HasMaxLength(100).IsRequired();
                doc.Property(e => e.Description).HasMaxLength(100).IsRequired();
                doc.Property(e => e.Type).HasMaxLength(100).IsRequired();



            }
            );
            modelBuilder.Entity<Prescription>(doc =>
            {
                doc.HasKey(e => e.IdPrescription);

                doc.Property(e => e.Date).IsRequired();
                doc.Property(e => e.DueDate).IsRequired();

                doc.HasOne(e => e.Doctor)
                .WithMany(e => e.DoctorPrescriptions)
                .HasForeignKey(e => e.IdDoctor)
                .OnDelete(DeleteBehavior.Cascade);

                doc.HasOne(e => e.Patient)
                .WithMany(e => e.PatientPrescriptions)
                .HasForeignKey(e => e.IdPatient)
                .OnDelete(DeleteBehavior.Cascade);



            }
            );
            modelBuilder.Entity<PrescriptionMedicament>(doc =>
            {
                doc.ToTable("Prescription_Medicament");

                doc.HasKey(e => new { e.IdMedicament, e.IdPrescription });

                doc.Property(e => e.Dose);
                doc.Property(e => e.Details).HasMaxLength(100).IsRequired();

                doc.HasOne(e => e.Medicament)
                .WithMany(e => e.MedicamentPrescriptionMedicaments)
                .HasForeignKey(e => e.IdMedicament)
                .OnDelete(DeleteBehavior.Cascade);

                doc.HasOne(e => e.Prescription)
                .WithMany(e => e.PrescriptionPrescriptionMedicaments)
                .HasForeignKey(e => e.IdPrescription)
                .OnDelete(DeleteBehavior.Cascade);


            }
            );
            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>(doc =>
            {
                doc.HasData(new List<Doctor>()
                {
                    new Doctor
                    {
                        IdDoctor = 1,
                        FirstName = "Jan",
                        LastName = "Kowalski",
                        Email = "jajajaja@jaja.pl"
                    },
                    new Doctor
                    {
                        IdDoctor = 2,
                        FirstName = "Marian",
                        LastName = "Kowalski",
                        Email = "mama.pl"
                    }
                });
            }
            );
            modelBuilder.Entity<Prescription>(doc =>
            {
                doc.HasData(new List<Prescription>()
                {
                    new Prescription
                    {
                        IdPrescription = 1,
                        Date = DateTime.Now,
                        DueDate = DateTime.Now,
                        IdDoctor = 1,
                        IdPatient = 1
                    },
                    new Prescription
                    {
                        IdPrescription = 2,
                        Date = DateTime.Now,
                        DueDate = DateTime.Now,
                        IdDoctor = 2,
                        IdPatient = 2
                    }
                });
            }
            );
            modelBuilder.Entity<Medicament>(doc =>
            {
                doc.HasData(new List<Medicament>()
                {
                    new Medicament
                    {
                        IdMedicament = 1,
                        Name = "Jajczan",
                        Description = "Zabija jajka",
                        Type = "Painkiller"
                    },
                    new Medicament
                    {
                        IdMedicament = 2,
                        Name = "Kawczan",
                        Description = "Zabija kawe",
                        Type = "Painkiller"
                    }
                });
            }
            );
            modelBuilder.Entity<Patient>(doc =>
            {
                doc.HasData(new List<Patient>()
                {
                    new Patient
                    {
                        IdPatient = 1,
                        FirstName = "Jan",
                        LastName = "Koka",
                        Birthdate = DateTime.Now
                    },
                    new Patient
                    {
                        IdPatient = 2,
                        FirstName = "Marian",
                        LastName = "Kowa",
                        Birthdate = DateTime.Now
                    }
                });
            }
            );
            modelBuilder.Entity<PrescriptionMedicament>(doc =>
            {
                doc.HasData(new List<PrescriptionMedicament>()
                {
                    new PrescriptionMedicament
                    {
                        IdMedicament = 1,
                        IdPrescription = 1,
                        Dose = 1,
                        Details = "jajeczka"
                    },
                    new PrescriptionMedicament
                    {
                        IdMedicament = 2,
                        IdPrescription = 2,
                        Dose = 2,
                        Details = "kaweczka"
                    }
                });
            }
            );
        }
    }
}
