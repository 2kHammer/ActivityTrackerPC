namespace ActivityTrackerPC.Tracking;


public interface IWindowChanged
{
    /*
     * Will be executed when the active window has changed
     */
    public void WindowChanged(string? nameNewWindow);
}