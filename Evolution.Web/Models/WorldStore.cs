using System.Collections.Generic;
using System.Linq;
using Evolution.Dtos;

namespace Evolution.Web.Models
{
    public class WorldStore
    {
        private Dictionary<string, List<AnimalDto>> AnimalsStore { get; } = new();
        private Dictionary<string, List<PlantDto>> PlantsStore { get; } = new();

        public WorldSizeDto WorldSize { get; set; } = new WorldSizeDto();
        public bool IsLoading { get; set; } = false;

        public void SetAnimals(List<AnimalDto> animals)
        {
            AnimalsStore.Clear();

            foreach (var animal in animals)
            {
                var key = GenerateLocationStoreKey(animal.Location);

                if (AnimalsStore.ContainsKey(key))
                {
                    AnimalsStore[key].Add(animal);
                }
                else
                {
                    AnimalsStore[key] = new List<AnimalDto> { animal };
                }
            }

        }

        public void SetPlants(List<PlantDto> plants)
        {
            PlantsStore.Clear();

            foreach (var plant in plants)
            {
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

        public List<AnimalDto> GetAnimalsAt(int row, int column)
        {
            var key = GenerateLocationStoreKey(row, column);
            return AnimalsStore.ContainsKey(key) ? AnimalsStore[key] : new List<AnimalDto>();
        }

        public List<AnimalDto> GetAllLiveAnimals()
        {
            return AnimalsStore.Values.SelectMany(l => l).Where(a => a.IsAlive).ToList();
        }

        public List<PlantDto> GetAllLivePlants()
        {
            return PlantsStore.Values.SelectMany(l => l).Where(a => a.IsAlive).ToList();
        }

        public List<PlantDto> GetPlantsAt(int row, int column)
        {
            var key = GenerateLocationStoreKey(row, column);
            return PlantsStore.ContainsKey(key) ? PlantsStore[key] : new List<PlantDto>();
        }

        private string GenerateLocationStoreKey(LocationDto location)
        {
            return GenerateLocationStoreKey(location.Row, location.Column);
        }

        private string GenerateLocationStoreKey(int row, int column)
        {
            const int NameFormattingDigitsCount = 4;
            return
                $"Cell ({row.ToString($"D{NameFormattingDigitsCount}")}, {column.ToString($"D{NameFormattingDigitsCount}")})";

        }

    }




}
