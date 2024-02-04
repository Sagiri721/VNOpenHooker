using System.Diagnostics;

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

        List<string>[] libraryPathMemory = new[] { new List<string>(), new List<string>() };

        static TrackingProcess trackingProcess = new TrackingProcess();

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {

            label1.Text = "Reading from: " + READ_FILE;

            // Load game library
            LoadLibraries();
            checkBox1.Checked = true;

            // Start threads
            button5_Click(sender, e);
            StartProcessTracking();
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

            foreach (string fileName in Directory.GetFiles(DLL_LIBRARY))
            {
                // Get File information
                FileInfo fileInfo = new FileInfo(fileName);
                string name = fileInfo.Name;

                //Skip unwanted files
                if (fileInfo.Extension != ".dll") continue;

                dllCollection.Items.Add(name);
                libraryPathMemory[1].Add(fileName);
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
    }
}