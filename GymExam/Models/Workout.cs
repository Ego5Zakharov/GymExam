namespace GymExam.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Time { get; set; }
        public string? startTrainingTime { get; set; }
        public TypeWorkout TypeWorkout { get; set; }
    }

    public enum TypeWorkout
    {
        personal = 0,
        group = 1,
    }
}
