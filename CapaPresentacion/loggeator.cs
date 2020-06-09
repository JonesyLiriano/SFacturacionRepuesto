using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion
{
    class loggeator
    {
        public static void EscribeEnArchivo(string contenido)
        {
            using (StreamWriter w = File.AppendText("C:/SFacturacionRepuesto/Logs.txt"))
            {
                Log(contenido + " - - - UserID: " + SFacturacion.Login.userID.ToString(), w);
            }

            using (StreamReader r = File.OpenText("C:/SFacturacionRepuesto/Logs.txt"))
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
    }
}
