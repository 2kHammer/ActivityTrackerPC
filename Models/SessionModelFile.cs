using System.Text.Json.Serialization;

namespace ActivityTrackerPC.Models
{
    public class SessionModelFile
    {
        
        [JsonInclude]
        public List<ApplicationModelFile> ApplicationsFile;
        public string User { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }

        [JsonIgnore] 
        private static Dictionary<string, string> newWindowNames = new Dictionary<string, string>()
        {
            { "", "Else" },
            { "java", "Rider" }
        };
       
        

            public SessionModelFile()
        {
            
        }

        public SessionModelFile(SessionModel model)  
        {
            User = model.User;
            StartingTime = model.StartingTime;
            EndingTime = model.EndingTime;
            ApplicationsFile = convertApplicationsModelForFile(model.Applications);
        }

        private List<ApplicationModelFile> convertApplicationsModelForFile(List<ApplicationModel> applist)
        {
            List<ApplicationModelFile> applistFiles = new List<ApplicationModelFile>();
            
            foreach(ApplicationModel app in applist)
            {
                TimeSpan duration = app.EndingTime - app.StartingTime;
                ApplicationModelFile ActapplistFile = applistFiles.Where(s => s.ApplicationName == app.ApplicationName).FirstOrDefault((ApplicationModelFile)null);
                if (ActapplistFile == null)
                {
                    applistFiles.Add(new ApplicationModelFile(app.ApplicationName, duration));
                    
                }
                else
                {
                    ActapplistFile.Duration += duration;
                }
            }

            applistFiles.ForEach(a =>
            {
                if (a.ApplicationName.Length != 0)
                    a.ApplicationName = a.ApplicationName.Remove(a.ApplicationName.Length - 1);

                //Umbenennen mancher Werte durch das Dictionary
                foreach (KeyValuePair<string, string> kvp in newWindowNames)
                {
                    if (a.ApplicationName == kvp.Key) a.ApplicationName = kvp.Value;
                }
                
            });
            
            return applistFiles;
        }

       
    }
}