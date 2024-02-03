using System.Diagnostics;

namespace gui
{

    public partial class Form1 : Form
    {
        const string READ_FILE = "D:\\TIAGO\\program\\text hooking\\vnhooker\\out.txt";
        const int SHIFT_JIS_ENCODING = 932;

        Task fileMonitoringThread = null;
        static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {

            label1.Text = "Reading from: " + READ_FILE;
            button5_Click(sender, e);
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
            Process.Start("Notepad", "\"" + READ_FILE + "\"");
        }
    }
}