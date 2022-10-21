using System.Diagnostics;
using System.Timers;

namespace ActivityTrackerPC.Tracking
{

    //Timer wird noch nicht aufgerufen
    
    public class WindowTracker
    {
        //
        private readonly int timerSpan = 1 * 1000;
        
        private string activeWindow;
        private IWindowChanged _windowObserver;
        private System.Timers.Timer windowPollTimer;

        public WindowTracker(IWindowChanged windowObserver)
        {
            //Sobald man System sieht ist PC an
            activeWindow = "System";
            //Wird über eine Änderung des Fenster benachrichtigt
            _windowObserver = windowObserver;


            windowPollTimer = new System.Timers.Timer();
            windowPollTimer.Interval = timerSpan;
            windowPollTimer.Elapsed += CheckWindow;
            windowPollTimer.Enabled = true;
            
            StartWindowPolling();
        }
        public static string RunCommandWithBash(string filename ,string command)          
        {                                                                   
            var psi = new ProcessStartInfo();                               
            psi.FileName = filename;                                        
            psi.Arguments = command;                                        
            psi.RedirectStandardOutput = true;                              
            psi.UseShellExecute = false;                                    
            psi.CreateNoWindow = true;                                      
                                                                    
            using var process = Process.Start(psi);                         
                                                                    
            process.WaitForExit();                                          
                                                                    
            var output = process.StandardOutput.ReadToEnd();                
                                                                    
            return output;                                                  
        }

        private string GetActiveWindow()
        {
            {
                string windowFocus = RunCommandWithBash("xdotool", $"getwindowfocus");
                string windowId = new string(
                    RunCommandWithBash("xdotool", "getwindowpid " + windowFocus).Where
                        (c => !char.IsWhiteSpace(c)).ToArray());
                return RunCommandWithBash("cat", "/proc/" + windowId + "/comm");
            } 
        }

        private void CheckWindow(Object s, ElapsedEventArgs e)
        {
            string windowNow = GetActiveWindow();
           
            if (!windowNow.Equals(activeWindow))
            {
                activeWindow = windowNow;
                _windowObserver.WindowChanged(windowNow);
               
            }
           
            
        }

        private void StartWindowPolling()
        {
            windowPollTimer.Start();
        }

        
    }
}