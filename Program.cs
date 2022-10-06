using System.Text.Json;
using ActivityTrackerPC.Models;
using ActivityTrackerPC.Tracking;
using ActivityTrackerPC.Writer;


//Variablen
int sessionBufferTime = 1000;
WindowChangeProcessor wcp = new WindowChangeProcessor();

//Holen der alten Sitzung aus Zwischenspeicher und und schreiben dieser in Datei
FileWriter fw = new FileWriter();
fw.writeLastSession();


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









































    