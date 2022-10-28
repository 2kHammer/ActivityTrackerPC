using System.Globalization;
using ActivityTrackerPC.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace ActivityTrackerPC.Writer
{
    /*
     * Convert the session data to the right format and appends to appropriate file (depends on date)
     */
    public class FileWriter
    {
        //path were the file are stored
        private readonly string filepath = "/home/alex/Entwicklung/TrackedTime/";
        private string Filename { get; set; }

        //Session data with the format for the file
        private SessionModelFile? _se;
        public SessionModelFile? LastSmf => _se;

        public FileWriter()
        {
            Filename = getfilename();
        }

        /*
         * Appends the data from the last session to the actual file (or creates a new one)
         */
        public void WriteLastSession()
        {
            
            
            List<SessionModelFile>? sessionList = new List<SessionModelFile>();
            string filelocation = filepath + Filename;
            
            //Reading in the json file
            if (File.Exists(filelocation))
            {
                using (StreamReader reader = File.OpenText(filelocation))
                {
                    string sessions = reader.ReadToEnd();
                    sessionList = JsonConvert.DeserializeObject<List<SessionModelFile>>(sessions);  //Fehler ist bei Umwandlung dieses Strings
                }
           
            }

            //Restore and convert the data from the last session
            SessionModelFile? smf = GetLastSessionModelFile();
            
            //Append the data from the last session and write it to the file
            if (smf != null)
            {
                sessionList?.Add(smf);

                string newSession = JsonConvert.SerializeObject(sessionList, Formatting.Indented);
                File.WriteAllText(filelocation, newSession);
            }


        }
        
        /*
         * Restores the data of the last session from the buffer and converts it to the correct file format 
         */
        private SessionModelFile? GetLastSessionModelFile()
        {
                string? lastSessionString = SessionInfoBuffer<string>.RestoreSessionInfo();
                if (lastSessionString == null) return null;
                SessionModel? sm = JsonSerializer.Deserialize<SessionModel>(lastSessionString);
                SessionModelFile smFile = new SessionModelFile(sm);
                _se = smFile;
                return smFile;
        }
        

        //Generates a file: Activity_Year_Week.json
        private string getfilename()
        {
            Calendar cal = CultureInfo.CurrentCulture.Calendar;
            string week = cal.GetWeekOfYear(DateTime.Now, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday).ToString();
            string year = DateTime.Now.Year.ToString();
            return "Activity_" + year + "_" + week + ".json";
        }

    }

}





