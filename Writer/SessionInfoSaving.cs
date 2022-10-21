using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ActivityTrackerPC.Models;

namespace ActivityTrackerPC.Writer
{
    public class SessionInfoSaving <T> where T: class
    {
        private static readonly string bufferpath = "SessionDataBuffer.dat";
        
        public static void BufferSessionInfo(T data) 
        {
            FileStream fs = new FileStream(bufferpath, FileMode.OpenOrCreate);

            try
            {
                
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, data);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to buffer. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
            
        }

        public static T RestoreSessionInfo() 
        {
            T restoredBuffer = null;
            //Prüfen ob die Datei zum Buffern der Session Daten schon existiert
            if (File.Exists(bufferpath))
            {
                FileStream fs = new FileStream(bufferpath, FileMode.Open);

                try
                {

                    BinaryFormatter formatter = new BinaryFormatter();
                    restoredBuffer = (T)formatter.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to restore. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
            else
            {
                File.Create(bufferpath);
            }

            return restoredBuffer;
        }
    }
}