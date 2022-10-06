using System.Text.Json.Serialization;

namespace ActivityTrackerPC.Models
{

    public class SessionModel
    {
        public SessionModel(string user, DateTime startingtime)
        {
            User = user;
            StartingTime = startingtime;
            Applications = new List<ApplicationModel>();
        }

        [JsonInclude]
        public List<ApplicationModel> Applications;
        public string User { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }
        

    }

}