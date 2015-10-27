using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace TimeLoggerManager
{
    public class Alpha
    {
        public void Beta()
        {
            while (true)
            { 
            }
        }
    };

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TLManager());

            //Alpha oAlpha = new Alpha();
            //Thread oThread = new Thread(new ThreadStart(oAlpha.Beta));
            //oThread.Start();
            //while (!oThread.IsAlive) ;
            //Thread.Sleep(1);

            Task t = Task.Factory.StartNew(() =>
            {
                string sharingTemp = "Test string";
                char[] c = new char[sharingTemp.Length];
                byte[] bytes = new byte[sharingTemp.Length + 1];
                bytes[0] = (byte) sharingTemp.Length;
                for (int i = 0; i < sharingTemp.Length; i++)
                {
                    c[i] = sharingTemp[i];
                    bytes[i + 1] = (byte) c[i];
                }
                MemoryMappedFile file = MemoryMappedFile.CreateOrOpen("MemoryFile", bytes.Length);
                MemoryMappedViewAccessor writer = file.CreateViewAccessor(0, bytes.Length);
                writer.WriteArray(0, bytes, 0, bytes.Length);

                Console.WriteLine("String to use in memory share: {0}", sharingTemp);
                Console.WriteLine("Length: {0}", bytes.Length);

                Console.WriteLine("Press any key to exit.");
            });
            //oThread.Abort();
            //oThread.Join();
        }
    }
}
