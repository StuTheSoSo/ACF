namespace BackEnd.Models
{
    public class StatsCollection
    {
        public int TotalCases { get; set; } = 0;
        public int PersonalCases { get; set; } = 0;

        public StatsCollection(int totalCases, int personalCases)
        {
            this.TotalCases = totalCases;
            this.PersonalCases = personalCases;
        }
    }
}
