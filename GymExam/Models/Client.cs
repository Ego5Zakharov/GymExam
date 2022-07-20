namespace GymExam.Models
{
    /// <summary>
    /// Модель клиента
    /// </summary>
    public class Client 
    {
        public int Id { get; set; }
        public string ?Name { get; set; }
        public DateTime Birthday { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        
    }
}
