using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Dtos;

namespace Evolution.Web.Models
{
    public class WorldStore
    {
        private Dictionary<Guid, AnimalDto> AnimalsStore { get; } = new();
        private Dictionary<string, List<PlantDto>> PlantsStore { get; } = new();
        public DateTime LastActionTime { get; private set; }

        public GameSettingsDto GameSettingsDto { get; set; } = new GameSettingsDto();

        public bool IsLoading { get; set; } = false;

        public void SetAnimals(List<AnimalDto> animals)
        {
            Console.WriteLine($"SetAnimals: {animals.Count}");
            foreach (var animal in animals)
            {
                Console.WriteLine($"A: {animal.Name} | R:{animal.Location.Row} | C:{animal.Location.Column} | {animal.IsAlive}");
                UpdateLastActionTime(animal.LastAction);
                var existed = AnimalsStore.ContainsKey(animal.Id);

                //if (!animal.IsAlive)
                //{
                //    AnimalsStore.Remove(animal.Id);
                //    continue;
                //}
                
                if (existed)
                {
                    AnimalsStore[animal.Id] = animal;
                }
                else
                {
                    AnimalsStore.Add(animal.Id, animal);
                }
            }

        }

        private void UpdateLastActionTime(DateTime t)
        {
            if (LastActionTime < t)
            {
                LastActionTime = t;
            }
        }

        public void SetPlants(List<PlantDto> plants)
        {
            PlantsStore.Clear();

            foreach (var plant in plants)
            {
                if (!plant.IsAlive) continue;

                var key = GenerateLocationStoreKey(plant.Location);

                if (PlantsStore.ContainsKey(key))
                {
                    PlantsStore[key].Add(plant);
                }
                else
                {
                    PlantsStore[key] = new List<PlantDto> { plant };
                }
            }
        }

        public List<AnimalDto> GetAnimals()
        {
            return AnimalsStore.Values.ToList();
        }

        public List<AnimalDto> GetAnimalsAt(int row, int column)
        {
            return AnimalsStore.Values.Where(a => a.Location.Row == row && a.Location.Column == column).ToList();
        }

        public List<PlantDto> GetPlantsAt(int row, int column)
        {
            var key = GenerateLocationStoreKey(row, column);
            return PlantsStore.ContainsKey(key) ? PlantsStore[key] : new List<PlantDto>();
        }

        public int GetAnimalsCount()
        {
            return AnimalsStore.Values.Count;
        }

        public double GetAnimalsAvgSpeed()
        {
            if (!AnimalsStore.Values.Any()) return 0;
            return AnimalsStore.Values.Average(a => a.Speed);
        }

        public int GetAvailableFood()
        {
            return PlantsStore.Values.Sum(l => l.Sum(p => p.Weight));
        }

        private string GenerateLocationStoreKey(LocationDto location)
        {
            return GenerateLocationStoreKey(location.Row, location.Column);
        }

        private string GenerateLocationStoreKey(int row, int column)
        {
            const int NameFormattingDigitsCount = 4;
            var key =
                $"Cell ({row}, {column})";
            return key;

        }


    }

}
