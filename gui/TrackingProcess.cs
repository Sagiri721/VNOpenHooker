using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gui
{
    internal class TrackingProcess
    {
        public int PID = 0;
        public string name = null;
        public string path = null;
        public Icon icon = null;

        private Process proc = null;

        public void Attach(int PID, string path)
        {
            this.PID = PID;
            this.path = path;

            name = new FileInfo(path).Name;
            icon = Icon.ExtractAssociatedIcon(path);

            proc = Process.GetProcessById(PID);
        }

        public void Detach()
        {
            this.PID = 0;
            this.path = null;
            this.name = null;
            this.icon = null;
            this.proc = null;
        }

        public void Track(Form1 parent)
        {
            while (true)
            {

                // Do the necessary checks for the process info
                if(name != null && path != null && PID != 0)
                {
                    // Chec kif exists
                    if (!Process.GetProcesses().Any(x => x.Id == PID)) Detach();
                }

                parent.UpdateTrackingInformation();
                Task.Delay(5000).Wait();
            }
        }
    }
}
