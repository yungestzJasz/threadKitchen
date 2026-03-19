using System.Collections.Generic;

namespace ThreadKitchen
{
    public class Chef
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dish { get; set; }
        public int Progress { get; set; }
        public bool IsFinished { get; set; }
        public double ElapsedSeconds { get; set; }

        // Messa qui come richiesto dal prof. 
        // L l'abbiamo resa 'public static' così il Form1 può leggerla facilmente.
        public static List<Chef> ChefsList = new List<Chef>
        {
            new Chef { Id = 0, Name = "Mario", Dish = "Lasagna" },
            new Chef { Id = 1, Name = "Giulia", Dish = "Risotto ai funghi" },
            new Chef { Id = 2, Name = "Luca", Dish = "Tiramisù" },
            new Chef { Id = 3, Name = "Federica", Dish = "Ossobuco alla milanese" }
        };
    }
}