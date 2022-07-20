
namespace GymExam.Models
{
    /// <summary>
    /// Модель администрации
    /// </summary>
    public class Administration
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Birthday { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        manager,
        mainManager
    }
}
