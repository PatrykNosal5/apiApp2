namespace WebApplication1.Models.DTOs
{
    public class PrescriptionToShow
    {
        public virtual  DoctorInPrescription doctor { get; set; } = null!;
        public virtual PatientInPrescription patient { get; set; } = null!;
        public virtual ICollection<MedicamentInPrescription> Medicaments { get; set; } = new List<MedicamentInPrescription>();
    }
}
