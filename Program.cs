using System.Text.Json;
using ActivityTrackerPC.Models;
using ActivityTrackerPC.Tracking;
using ActivityTrackerPC.Writer;
using ActivityTrackerPC.Writer.DB;


//Variablen
int sessionBufferTime = 1000;
WindowChangeProcessor wcp = new WindowChangeProcessor();

//Holen der alten Sitzung aus Zwischenspeicher und und schreiben dieser in Datei
FileWriter fw = new FileWriter();
fw.writeLastSession();

//Schreiben in Datenbank
if (fw.lastSMF != null)
{
    SessionRepository sr = new SessionRepository();
    sr.addSession(fw.lastSMF);
}


//Buffern der aktuellen Sitzung
System.Timers.Timer saveDataLastSession = new System.Timers.Timer(sessionBufferTime);
saveDataLastSession.Elapsed += (sender, eventArgs) =>
{
    wcp.updateEndingTime();
    SessionInfoSaving<string>.BufferSessionInfo(JsonSerializer.Serialize(wcp.actSession));
   
};
saveDataLastSession.Start();

//Bis PC abgeschaltet wird
while (true)
{
    
}









































    