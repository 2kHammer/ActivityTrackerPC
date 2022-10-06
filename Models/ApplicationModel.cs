namespace ActivityTrackerPC.Models
{

    public class ApplicationModel
    {
        public ApplicationModel(string applicationname, DateTime startingtime)
        {
            ApplicationName = applicationname;
            StartingTime = startingtime;
        }
        public string ApplicationName { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }
    }

}