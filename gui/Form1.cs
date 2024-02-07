using System.Diagnostics;
using System.Windows.Forms;
using IWshRuntimeLibrary; /*Windows Script Host Object Model*/

namespace gui
{

    public partial class Form1 : Form
    {
        public static string SOURCE = "D:\\TIAGO\\program\\text hooking\\vnhooker";
        public static int SHIFT_JIS_ENCODING = 932;

        const string DLL_LIBRARY = "D:\\TIAGO\\program\\text hooking\\vnhooker\\createhook\\Release";
        const string READ_FILE = "D:\\TIAGO\\program\\text hooking\\vnhooker\\out.txt";

        Task fileMonitoringThread = null, processTrackingThread = null;
        static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        List<string>[] libraryPathMemory = new[] { new List<string>(), new List<string>(), new List<string>() };

        static TrackingProcess trackingProcess = new TrackingProcess();

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Load game library
            LoadLibraries();
            checkBox1.Checked = true;

            // Start threads
            Connection.LoadSettings();
            StartProcessTracking();

            macroCollection.SelectedIndex = 0;
        }

        private async void StartProcessTracking()
        {

            // Start process tracking
            processTrackingThread = Task.Run(() => trackingProcess.Track(this));
        }

        private void LoadLibraries()
        {
            // Clear previous information
            gameCollection.Items.Clear();
            dllCollection.Items.Clear();
            macroCollection.Items.Clear();

            foreach (var collection in libraryPathMemory) { collection.Clear(); }

            string gameLib = SOURCE + "\\library";
            foreach (string fileName in Directory.GetFiles(gameLib))
            {

                // Get File information
                FileInfo fileInfo = new FileInfo(fileName);
                string name = fileInfo.Name;
                if (fileInfo.Name.Contains(" - Shortcut"))
                {
                    // Clean file name
                    name = name.Replace(" - Shortcut", "");

                    // Replace file
                    string newFileName = fileName.Replace(" - Shortcut", "");
                    System.IO.File.Move(fileName, newFileName);
                }

                gameCollection.Items.Add(name);
                // Keep track of internal library
                libraryPathMemory[0].Add(fileName);
            }

            List<string> completeFiles = Directory.GetFiles(DLL_LIBRARY).ToList();
            completeFiles.AddRange(Directory.GetFiles(SOURCE).ToList());
            completeFiles.AddRange(Directory.GetFiles(SOURCE + "\\macros").ToList());

            foreach (string fileName in completeFiles)
            {

                // Get File information
                FileInfo fileInfo = new FileInfo(fileName);
                string name = fileInfo.Name;

                //Skip unwanted files
                switch (fileInfo.Extension)
                {
                    case ".dll":
                        dllCollection.Items.Add(name);
                        libraryPathMemory[1].Add(fileName);
                        break;

                    case ".cmd":
                    case ".bat":
                    case ".btm":
                    case ".sh":
                        macroCollection.Items.Add(name);
                        libraryPathMemory[2].Add(fileName);
                        break;
                }

            }

            toolStripStatusLabel1.Text = "Libraries loaded";
        }

        async void MonitorFileChanges(CancellationTokenSource token)
        {
            using (var fileStream = new FileStream(READ_FILE, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(fileStream, System.Text.Encoding.UTF8))
            {
                while (!token.IsCancellationRequested)
                {

                    fileStream.Seek(0, SeekOrigin.Begin);
                    string fileContent = reader.ReadToEnd();

                    textMonitor.Text = fileContent;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //File.WriteAllText(READ_FILE, string.Empty);
            textMonitor.Clear();
            toolStripStatusLabel1.Text = "Output stream is clean";
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            /*
            // File monitoring
            fileMonitoringThread = Task.Run(() => MonitorFileChanges(cancellationTokenSource));
            */

            Connection.Connect(toolStripStatusLabel1, conn, textMonitor);

            if (Connection.tcpClient != null)
            {
                close.Enabled = true;
                open.Enabled = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //cancellationTokenSource.Cancel();

            Connection.Close(conn);

            if (Connection.tcpClient == null)
            {
                close.Enabled = false;
                open.Enabled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Opening output stream";
            Process.Start("Notepad", "\"" + READ_FILE + "\"");
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadLibraries();
            toolStripStatusLabel1.Text = "Game library reloaded";
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (gameCollection.SelectedIndex == -1)
            {
                MessageBox.Show("Select a game first", "SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string gamePath = libraryPathMemory[0][gameCollection.SelectedIndex];
            int pid = 0;

            if (!string.IsNullOrEmpty(gamePath) && System.IO.File.Exists(gamePath))
            {

                using (Process proc = new Process())
                {

                    proc.StartInfo.FileName = gameCollection.SelectedItem.ToString();
                    proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(gamePath);

                    proc.StartInfo.UseShellExecute = true;
                    proc.Start();

                    pid = proc.Id;
                }
            }

            toolStripStatusLabel1.Text = "Starting " + gamePath + " as process " + pid;
            trackingProcess.Attach(pid, gamePath);

            if (checkBox1.Checked)
            {
                Clipboard.SetText(pid.ToString());
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("Explorer", "\"" + SOURCE + "\\library\"");
        }

        public void UpdateTrackingInformation()
        {
            if (!String.IsNullOrEmpty(trackingProcess.path))
            {
                tracker.Text = trackingProcess.name + " (" + trackingProcess.PID + ")";
                pictureBox1.Image = trackingProcess.icon.ToBitmap();

            }
            else
            {
                tracker.Text = "None";
                pictureBox1.Image = null;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            trackingProcess.Detach();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            /* TODO process selection screen */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Check for tracked process
            if (trackingProcess.path == null)
            {
                MessageBox.Show("Start tracking a process before injecting", "SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dllCollection.SelectedIndex == -1)
            {
                MessageBox.Show("Select a dll to inject first", "SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Inject the dll
            //Recompile the injector
            if (checkBox2.Checked) RunMacro("compile_injector.cmd", true);
            // Run the executable
            using (Process proc = new Process())
            {

                proc.StartInfo.FileName = "evil.exe";
                proc.StartInfo.WorkingDirectory = SOURCE;
                proc.StartInfo.ArgumentList.Add(trackingProcess.PID.ToString());
                proc.StartInfo.ArgumentList.Add(" \"" + libraryPathMemory[1][dllCollection.SelectedIndex] + "\"");

                proc.StartInfo.UseShellExecute = true;
                proc.Start();

                proc.WaitForExit();
            }
        }

        private int RunMacro(string name, bool wait)
        {
            string scriptPath = "";
            for (int i = 0; i < macroCollection.Items.Count; i++)
            {
                if (macroCollection.Items[i].Equals(name))
                {
                    scriptPath = libraryPathMemory[2][i];
                    break;
                }
            }

            int pid = 0;
            if (!string.IsNullOrEmpty(scriptPath) && System.IO.File.Exists(scriptPath))
            {

                using (Process proc = new Process())
                {

                    proc.StartInfo.FileName = new FileInfo(scriptPath).Name;
                    proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(scriptPath);

                    proc.StartInfo.UseShellExecute = true;
                    proc.StartInfo.CreateNoWindow = false;
                    proc.Start();
                    pid = proc.Id;

                    if (wait) proc.WaitForExit();
                }
            }
            else
            {
                MessageBox.Show("No macro found", "SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }

            toolStripStatusLabel1.Text = "Runnning " + scriptPath;
            return pid;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (macroCollection.SelectedIndex == -1)
            {
                MessageBox.Show("Select a script first", "SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string scriptPath = libraryPathMemory[2][macroCollection.SelectedIndex];
            if (!string.IsNullOrEmpty(scriptPath) && System.IO.File.Exists(scriptPath))
            {

                using (Process proc = new Process())
                {

                    proc.StartInfo.FileName = macroCollection.SelectedItem.ToString();
                    proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(scriptPath);

                    proc.StartInfo.UseShellExecute = true;
                    proc.StartInfo.CreateNoWindow = false;
                    proc.Start();
                }
            }

            toolStripStatusLabel1.Text = "Runnning " + scriptPath;
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunMacro("run_server.cmd", false);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var uri = @"https://www.nodejs.org/en";
            var psi = new ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            Process.Start(psi);
        }

        private void addGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog gameSelect = new OpenFileDialog();
            /* Define settings */
            gameSelect.RestoreDirectory = true;
            gameSelect.Title = "Select a game";
            gameSelect.Filter = "exe files (*.exe)|*.exe";
            
            if(gameSelect.ShowDialog() == DialogResult.OK)
            {
                string copyTarget = gameSelect.FileName;

                // Create shortcut
                string shortcut = SOURCE + "\\library\\" + new FileInfo(copyTarget).Name + ".lnk";

                // Why the fuck is creating windows shortcuts so complicated?
                // Thankfully there is a very funny workaround for creating shortcuts

                try
                {
                    var shell = new WshShell();
                    IWshShortcut shrtct = (IWshShortcut)shell.CreateShortcut(shortcut);
                    shrtct.TargetPath = copyTarget;
                    shrtct.Save();

                    toolStripStatusLabel1.Text = $"Shortcut created at: {shortcut}";
                    LoadLibraries();

                }
                catch(Exception ex)
                {
                    MessageBox.Show("Couldn't create the shortcut");
                }
            }
        }
    }
}