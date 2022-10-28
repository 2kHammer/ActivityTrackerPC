using ActivityTrackerPC.Models;

namespace ActivityTrackerPC.Tracking
{
    /*
     * Process the change of the current window
     */
    public class WindowChangeProcessor : IWindowChanged
    {
        
        
        //Session and application data is saved here
        public SessionModel ActSession { get;  }
        public WindowChangeProcessor()
        {
            //Notifies if the window has changed
            WindowTracker tracker = new WindowTracker(this);
            
            
            //get the active user
            string? user = WindowTracker.RunCommandWithBash("whoami", "");
            user = user.Remove(user.Length - 1);
            
            
            ActSession = new SessionModel(user, DateTime.Now);
        }

        /*
         * Sets the ending time of the session for the session and the last application
         */
        public void UpdateEndingTime()
        {
            ActSession.EndingTime = DateTime.Now;
            ActSession.Applications.LastOrDefault()!.EndingTime = ActSession.EndingTime;
        }
        
       //Is executed when "tracker" recognize a window change
        public void WindowChanged(string? nameNewWindow)
        {
                //Add the ending time to the previous application
                if (ActSession.Applications.LastOrDefault() != null)
                {
                    ActSession.Applications.Last().EndingTime = DateTime.Now;
                }

                //Add actual Application to the end of the list
                ApplicationModel actApplication = new ApplicationModel(nameNewWindow, DateTime.Now);
                ActSession.Applications.Add(actApplication);

        }
    }

}