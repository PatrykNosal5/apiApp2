namespace WebApplication1.Models
{
    public class Patient
    {
        public int IdPatient{ get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime Birthdate { get; set; }

        public virtual ICollection<Prescription> PatientPrescriptions{ get; set; } = new List<Prescription>();
    }
}
