using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ActivityTrackerPC.Writer
{
    /*
     * Save and restore data to a binary bufferfile
     */
    public class SessionInfoBuffer <T> where T: class
    {
        //Filepath of the binary buffer file
        private static readonly string bufferpath = "SessionDataBuffer.dat";
        
        
        //Save the data
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

        //Restore the data
        public static T? RestoreSessionInfo() 
        {
            T? restoredBuffer = null;
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