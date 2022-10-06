using System.Text;
using System.Text.Json;
using ActivityTrackerPC.Models;

namespace ActivityTrackerPC.Tracking
{

    public class WindowChangeProcessor : IWindowChanged
    {
        private WindowTracker tracker;
        private string actualWindow;
        public SessionModel actSession { get;  }
        public WindowChangeProcessor()
        {
            tracker = new WindowTracker(this);
            actualWindow = "";
            actSession = new SessionModel("Alex", DateTime.Now);
        }

        public void updateEndingTime()
        {
            actSession.EndingTime = DateTime.Now;
            actSession.Applications.LastOrDefault().EndingTime = actSession.EndingTime;
        }
        public void WindowChanged(string nameNewWindow)
        {
            //Add the ending time to the previous application
                if (actSession.Applications.LastOrDefault() != null)
                {
                    actSession.Applications.Last().EndingTime = DateTime.Now;
                }

                //Add actual Application to the end of the list
                ApplicationModel actApplication = new ApplicationModel(nameNewWindow, DateTime.Now);
                actSession.Applications.Add(actApplication);

        }
    }

}