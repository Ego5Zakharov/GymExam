using GymExam.Models;

namespace GymExam.Data
{
    public class DbInit
    {
        static public void Initialize(GymContext context)
        {
            if (context.Administration.Any())
            {
                return;   // DB has been seeded
            }
            
            var administrations = new Administration[]
              {
                 new Administration { Birthday = DateTime.Parse("2012-09-01"), Email = "egorAdmin@mail.ru", Gender = "Male", Name ="Egor", Phone ="xxx", Status = Status.mainManager},
              };

            var coaches = new Coach[]
              {
                 new Coach { Birthday = DateTime.Parse("2012-09-01"), Email = "egorCoach@mail.ru", Gender = "Male", Name ="Egor", Phone ="xxx",Specialization = Specialization.dances, TrainingExperience = "15 лет"},
              };

            var clients = new Client[]
            {
                new Client  {Birthday = DateTime.Parse("2012-09-01"), Email = "egorUser@mail.ru", Gender = "Male", Name ="Egor", Phone ="xxx"}
            };


            foreach (Administration admins in administrations)
            {
                context.Administration.Add(admins);
            }

            foreach (Coach coach in coaches)
            {
                context.Coaches.Add(coach);
            }


            context.SaveChanges();
        }

    }
}
