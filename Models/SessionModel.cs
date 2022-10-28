using System.Text.Json.Serialization;

namespace ActivityTrackerPC.Models
{
    /*
     * Used to store the session data in the code
     */
    public class SessionModel
    {
        public SessionModel(string? user, DateTime startingtime)
        {
            User = user;
            StartingTime = startingtime;
            Applications = new List<ApplicationModel>();
        }

        /*
         * List of all used Applications from a Session
         * (the same applications can occur more often)
         */
        [JsonInclude]
        public List<ApplicationModel> Applications;
        public string? User { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }
        

    }

}