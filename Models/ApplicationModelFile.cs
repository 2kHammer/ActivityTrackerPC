namespace ActivityTrackerPC.Models;
    
    /*
     * Used to store the application data in a file
     */
public class ApplicationModelFile
{
    
    public string? ApplicationName { get; set; }
    public TimeSpan Duration { get; set; }

    public ApplicationModelFile(string? applicationname, TimeSpan duration)
    {
        ApplicationName = applicationname;
        Duration = duration;
    }
}