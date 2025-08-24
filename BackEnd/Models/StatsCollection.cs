namespace BackEnd.Models
{
    public class StatsCollection
    {
        /// <summary> Initializes a new instance of the <see cref="StatsCollection"/> class. </summary>
        /// <param name="totalCases">    The total cases. </param>
        /// <param name="personalCases"> The personal cases. </param>
        public StatsCollection(int totalCases, int personalCases)
        {
            this.TotalCases = totalCases;
            this.PersonalCases = personalCases;
        }

        /// <summary> Gets or sets the personal cases. </summary>
        /// <value> The personal cases. </value>
        public int PersonalCases { get; set; } = 0;

        /// <summary> Gets or sets the total cases. </summary>
        /// <value> The total cases. </value>
        public int TotalCases { get; set; } = 0;
    }
}