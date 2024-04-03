namespace BeautySalonWebApplication.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime BookingTime { get; set; }

        public string PhoneNumber { get; set; }

        public Appointment()
        {
            
        }

    }
}
