using System.Diagnostics;
using System.Timers;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace ActivityTrackerPC.Tracking
{

   
    /*
     * Checks the current window and notify an registered IWindowChanged object if
     * the window has changed.
     */
    public class WindowTracker
    {
        //frequency of window check in ms
        private readonly int timerSpan = 1 * 1000;
        
        private string? _activeWindow;
        
        //is notified when the window changes
        private IWindowChanged _windowObserver;
        
        
        //Timer for window checking is started and the window observer gets registered
        public WindowTracker(IWindowChanged windowObserver)
        {
            //Default value
            _activeWindow = "System";
            
            _windowObserver = windowObserver;

            //Timer init
            System.Timers.Timer windowPollTimer = new System.Timers.Timer();
            windowPollTimer.Interval = timerSpan;
            windowPollTimer.Elapsed += CheckWindow;
            windowPollTimer.Enabled = true;
            windowPollTimer.Start();
        }
        
        //Executes a bash command and returns the result as string
        public static string? RunCommandWithBash(string programName ,string command)          
        {                                                                   
            var psi = new ProcessStartInfo();                               
            psi.FileName = programName;                                        
            psi.Arguments = command;                                        
            psi.RedirectStandardOutput = true;                              
            psi.UseShellExecute = false;                                    
            psi.CreateNoWindow = true;                                      
                                                                    
            using (var process = Process.Start(psi)){

                process!.WaitForExit();

                var output = process.StandardOutput.ReadToEnd();

                return output;
            }
        }

        //Returns the name of the active window with a few bash commands
        private string? GetActiveWindow()
        {
                string? windowFocus = RunCommandWithBash("xdotool", $"getwindowfocus");
                string windowId = new string(
                    RunCommandWithBash("xdotool", "getwindowpid " + windowFocus).Where
                        (c => !char.IsWhiteSpace(c)).ToArray());
                return RunCommandWithBash("cat", "/proc/" + windowId + "/comm");

                
        }

        
        /*
         * Is executed by the timer. Checks if the actual window
         * is equal to the window from the last check. Notifies
         * the IWindowChanged object if not.
         */
        private void CheckWindow(object? s, ElapsedEventArgs e)
        {
            string? windowNow = GetActiveWindow();

            if (!windowNow.Equals(_activeWindow))
            {
                _activeWindow = windowNow;
                _windowObserver.WindowChanged(windowNow);
               
            }
           
            
        }


        
    }
}