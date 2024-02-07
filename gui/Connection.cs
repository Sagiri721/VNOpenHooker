using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace gui
{
    
    public struct SettingsS
    {
        public int port;
        public string host;

        public SettingsS(int port, string host) : this()
        {
            this.port = port;
            this.host = host;
        }
    }

    internal class Connection
    {
        public static byte[] SEND_ORIGIN = {69};

        private static SettingsS settings;
        public static SettingsS Settings { get; set; }

        public static TcpClient tcpClient = null;

        private static string file = Form1.SOURCE + "\\server\\hosting_settings.json";
        public static void LoadSettings()
        {
            try
            {

                string jsonString = File.ReadAllText(file);
                settings = JsonSerializer.Deserialize<SettingsS>(jsonString);

            }catch(Exception e) {
                MessageBox.Show(e.Message, "SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static async void Connect(ToolStripStatusLabel toolStripStatusLabel1, Label conn, RichTextBox textMonitor)
        {
            if (settings.host == null) LoadSettings();
            if(settings.host == null)
            {
                MessageBox.Show("Could not read connection settings, defaulting to static settings", "SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                settings = new SettingsS(8080, "127.0.0.1");
                return;
            }

            // Perform connection or something
            try
            {

                toolStripStatusLabel1.Text = "Connecting to " + settings.host;
                tcpClient = new TcpClient(settings.host, settings.port);

                conn.Text = "Connected! :3";
                conn.ForeColor = Color.Green;

                tcpClient.Client.Send(SEND_ORIGIN);

                // Start a separate thread to continuously read messages
                Task.Run(() => ReceiveMessages(textMonitor));

            }
            catch(Exception e)
            {
                tcpClient = null;

                MessageBox.Show("Could not connect to server", "SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(e.Message, "SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private static async Task ReceiveMessages(RichTextBox textMonitor)
        {
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();

                while (true)
                {
                    
                    byte[] buffer = new byte[1024];
                    int bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        HandleReceivedMessage(receivedMessage, textMonitor);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while receiving messages: " + ex.Message);
            }
        }

        private static void HandleReceivedMessage(string message, RichTextBox textMonitor)
        {
            textMonitor.AppendText("Received: " + message + Environment.NewLine);
        }

        public static void Close(Label conn)
        {
            tcpClient.Close();
            tcpClient = null;

            conn.Text = "Closed";
            conn.ForeColor = Color.Red;
        }
    }
}
