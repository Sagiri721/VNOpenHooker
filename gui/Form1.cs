using System.Diagnostics;
using System.Security.Cryptography;

namespace gui
{

    public partial class Form1 : Form
    {
        const string SOURCE = "D:\\TIAGO\\program\\text hooking\\vnhooker";
        const string DLL_LIBRARY = "D:\\TIAGO\\program\\text hooking\\vnhooker\\createhook\\Release";
        const string READ_FILE = "D:\\TIAGO\\program\\text hooking\\vnhooker\\out.txt";
        const int SHIFT_JIS_ENCODING = 932;

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

            label1.Text = "Reading from: " + READ_FILE;

            // Load game library
            LoadLibraries();
            checkBox1.Checked = true;

            // Start threads
            button5_Click(sender, e);
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

            const string gameLib = SOURCE + "\\library";
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
            using (var reader = new StreamReader(fileStream, System.Text.Encoding.GetEncoding(SHIFT_JIS_ENCODING)))
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
            File.WriteAllText(READ_FILE, string.Empty);
            toolStripStatusLabel1.Text = "Output stream is clean";
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            if (!cancellationTokenSource.IsCancellationRequested && fileMonitoringThread != null)
            {
                MessageBox.Show("Task is still running");
                return;
            }

            // File monitoring
            fileMonitoringThread = Task.Run(() => MonitorFileChanges(cancellationTokenSource));
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (cancellationTokenSource.IsCancellationRequested)
            {
                MessageBox.Show("Already canceled");
                return;
            }

            cancellationTokenSource.Cancel();
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

            if (!string.IsNullOrEmpty(gamePath) && File.Exists(gamePath))
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

        private void RunMacro(string name, bool wait)
        {
            int index = -1;
            for (int i = 0; i < macroCollection.Items.Count; i++)
            {
                if (macroCollection.Items[i].Equals(name))
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                MessageBox.Show("No macro found", "SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string scriptPath = libraryPathMemory[2][index];
            if (!string.IsNullOrEmpty(scriptPath) && File.Exists(scriptPath))
            {

                using (Process proc = new Process())
                {

                    proc.StartInfo.FileName = macroCollection.SelectedItem.ToString();
                    proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(scriptPath);

                    proc.StartInfo.UseShellExecute = true;
                    proc.StartInfo.CreateNoWindow = false;
                    proc.Start();

                    if (wait) proc.WaitForExit();
                }
            }

            toolStripStatusLabel1.Text = "Runnning " + scriptPath;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (macroCollection.SelectedIndex == -1)
            {
                MessageBox.Show("Select a script first", "SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string scriptPath = libraryPathMemory[2][macroCollection.SelectedIndex];
            if (!string.IsNullOrEmpty(scriptPath) && File.Exists(scriptPath))
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
    }
}