namespace GymExam.Models
{
    /// <summary>
    /// Модель тренера
    /// </summary>
    public class Coach 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Birthday { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? TrainingExperience { get; set; }
        public CoachStatus Status { get; set; }
        public Specialization Specialization { get; set; }
    }

    public enum CoachStatus
    {
        coach = 0,
        olderCoach = 1,
        mainCoach = 3,
    }

    public enum Specialization
    {
        trainingSessions,
        dances,
        yoga,

    }
}
