namespace ActivityTrackerPC.Models;

public class ApplicationModelFile
{
    public string ApplicationName { get; set; }
    public TimeSpan Duration { get; set; }

    public ApplicationModelFile(string applicationname, TimeSpan duration)
    {
        ApplicationName = applicationname;
        Duration = duration;
    }
}