using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion
{
    class Loggeator
    {
        public static void EscribeEnArchivo(string contenido)
        {
            EmptyLog();

            using (StreamWriter w = File.AppendText("C:/SFacturacion/Logs.txt"))
            {
                Log(contenido + " - - - UserID: " + SFacturacion.Login.userID.ToString(), w);
            }

            using (StreamReader r = File.OpenText("C:/SFacturacion/Logs.txt"))
            {
                DumpLog(r);
            }
        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }

        private static void EmptyLog()
        {
            if (new FileInfo("C:/SFacturacion/Logs.txt").Length > 100000)
            {
                File.WriteAllText("C:/SFacturacion/Logs.txt", String.Empty);
            }

        }
    }
}
