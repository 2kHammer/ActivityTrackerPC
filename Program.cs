using System.Text.Json;
using ActivityTrackerPC.Tracking;
using ActivityTrackerPC.Writer;
using ActivityTrackerPC.Writer.DB;


//interval the actual session data is buffered
int sessionBufferTime = 1000;
//Contains the info about window changes
WindowChangeProcessor wcp = new WindowChangeProcessor();

//Load the data of the old session from the buffer and write it to the json file
FileWriter fw = new FileWriter();
fw.WriteLastSession();

//Schreiben in Datenbank
if (fw.LastSmf != null)
{
    SessionRepository sr = new SessionRepository();
    sr.addSession(fw.LastSmf);
}


/*
 * Update the ending time and buffer the data of the current session in an interval of "sessionBufferTime" ms
 * (when the pc is turned off the session data (from last buffer) is saved, and can be processed (json, database)
 *  the next time the program is started)
*/
System.Timers.Timer saveDataLastSession = new System.Timers.Timer(sessionBufferTime);
saveDataLastSession.Elapsed += (_, _) =>
{
    wcp.UpdateEndingTime();
    SessionInfoBuffer<string>.BufferSessionInfo(JsonSerializer.Serialize(wcp.ActSession));
   
};
saveDataLastSession.Start();

//endless loop
while (true)
{
    
}









































    