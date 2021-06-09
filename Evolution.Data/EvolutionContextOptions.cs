namespace Evolution.Data
{
    public class EvolutionContextOptions
    {
        public EvolutionContextOptions(string connectionString, bool useConsoleLogger)
        {
            ConnectionString = connectionString;
            UseConsoleLogger = useConsoleLogger;
        }

        public string ConnectionString { get; set; }
        public bool UseConsoleLogger { get; set; }
    }
}