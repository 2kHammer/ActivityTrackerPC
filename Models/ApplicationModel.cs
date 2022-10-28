namespace ActivityTrackerPC.Models
{   
    /*
     * Used to store the data of an application in code,
     * Every window change a new applciation is created (they can have the same application name)
     */

    public class ApplicationModel
    {
        public ApplicationModel(string? applicationname, DateTime startingtime)
        {
            ApplicationName = applicationname;
            StartingTime = startingtime;
        }
        public string? ApplicationName { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }
    }

}