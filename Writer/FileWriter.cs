using System.Globalization;
using ActivityTrackerPC.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace ActivityTrackerPC.Writer
{

    public class FileWriter
    {
        private readonly string filepath = "/home/alex/Entwicklung/TrackedTime/";

        private string filename { get; set; }

        public FileWriter()
        {
            filename = getfilename();
        }

        public void writeLastSession()
        {
            
            //Holen der bereits geschriebenen Session
            List<SessionModelFile> sessionList = new List<SessionModelFile>();
            string filelocation = filepath + filename;
            
            //Datei einlesen und nach Json umwandeln
            if (File.Exists(filelocation))
            {
                using (StreamReader reader = File.OpenText(filelocation))
                {
                    string sessions = reader.ReadToEnd();
                    sessionList = JsonConvert.DeserializeObject<List<SessionModelFile>>(sessions);  //Fehler ist bei Umwandlung dieses Strings
                }
           
            }

            //Holen der letzten Session (ist noch nicht geschrieben)
            SessionModelFile smf = getLastSessionModelFile();
            
            //Schreiben aller Sessions
            if (smf != null)
            {
                sessionList.Add(smf);

                string newSession = JsonConvert.SerializeObject(sessionList, Formatting.Indented);
                File.WriteAllText(filelocation, newSession);
            }


        }

        private SessionModelFile getLastSessionModelFile()
        {
            try
            {
                string lastSessionString = SessionInfoSaving<string>.RestoreSessionInfo();
                SessionModel sm = JsonSerializer.Deserialize<SessionModel>(lastSessionString);
                SessionModelFile smFile = new SessionModelFile(sm);
                return smFile;
            }
            catch (Exception ex)
            {
                Console.Write($"Keine Daten der letzten Sitzung vorhanden: { ex}");
                return null;
            }
        }

        private string getfilename()
        {
            Calendar cal = CultureInfo.CurrentCulture.Calendar;
            string week = cal.GetWeekOfYear(DateTime.Now, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday).ToString();
            string year = DateTime.Now.Year.ToString();
            return "Activity_" + year + "_" + week + ".json";
        }

    }

}





