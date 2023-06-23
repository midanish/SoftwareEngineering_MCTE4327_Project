using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{

    public enum Pengguna { Head, body };
    public partial class Form_Server : Form
    {
        private TcpListener server;
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private bool isConnected = false;

        private readonly object ClipboardLock = new object();
        
        public Form_Server()
        {
            InitializeComponent();
        }

        private void MessagePopup(string message, Pengguna user)
        {
            var time = DateTime.Now.ToString("HH:mm");
            

            var chatBubble = new Panel();
            

            var messageLabel = new Label();
            messageLabel.Text = message;
            messageLabel.Font = new Font("Segoe UI", 12);
            messageLabel.ForeColor = Color.Transparent;
            //messageLabel.Padding = new Padding(10);
            messageLabel.MaximumSize = new Size(250, 0);
            messageLabel.AutoSize = true;

            var timeLabel = new Label();
            timeLabel.Text = time;
            timeLabel.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            timeLabel.ForeColor = Color.Gray;
            //timeLabel.Padding = new Padding(10, 0, 10, 10);
            timeLabel.AutoSize = true;

            //chatBubble.Controls.Add(messageLabel);
            //chatBubble.Controls.Add(timeLabel);

            richTextBox_Display.Invoke((MethodInvoker)(() =>
            {
                var sendColor = user == Pengguna.Head ? Color.Yellow : Color.Aqua;
                richTextBox_Display.BorderStyle = BorderStyle.Fixed3D;
                richTextBox_Display.AutoSize = true;
                //Shortcuts like ctrl + C can be used
                richTextBox_Display.ShortcutsEnabled = true;
                //richTextBox_Display.MaximumSize = new Size(200, Height);
                richTextBox_Display.SelectionAlignment = user == Pengguna.Head ? HorizontalAlignment.Right : HorizontalAlignment.Left;
                //richTextBox_Display.Controls.Add(chatBubble);
                //chatBubble.BringToFront();
                richTextBox_Display.AppendText(message + " ");
                richTextBox_Display.AppendText(" " + time);
                richTextBox_Display.AppendText(Environment.NewLine);
                richTextBox_Display.ScrollToCaret();
            }));
        }

        private void textBox_Text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (isConnected)
                {
                    string message = textBox_Text.Text;
                    SendMessage(message);
                    textBox_Text.Clear();
                    e.Handled = true;
                }
                else
                {
                    MessageBox.Show("No client connected. Start the server and wait for a client to connect.");
                }
            }
        }

        private void Form_Server_Load(object sender, EventArgs e)
        {
            // Set up your IP address and port
            string ipAddress = "127.0.0.1";
            int port = 12345;

            // Initialize server
            try
            {
                server = new TcpListener(IPAddress.Parse(ipAddress), port);
                server.Start();

                // Start listening for incoming client connections
                Task.Run(() => AcceptClients());

                label_Status.Text = "DISCONNECTED";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting server: " + ex.Message);
            }
        }

        private void richTextBox_Display_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_Upload_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    string chatHistory = richTextBox_Display.Text;
                    System.IO.File.WriteAllText(filePath, chatHistory);
                    MessageBox.Show("Chat history saved successfully!");
                }
            }
            else
            {
                MessageBox.Show("No client connected. Start the server and wait for a client to connect.");
            }
        }

        private void textBox_Text_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_Send_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                string message = textBox_Text.Text;
                SendMessage(message);
                textBox_Text.Clear();
                MessagePopup(textBox_Text.Text, Pengguna.Head);
            }
            else
            {
                MessageBox.Show("No client connected. Start the server and wait for a client to connect.");
            }
        }

        private void pictureBox_Image_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif) | *.jpg; *.jpeg; *.png; *.gif";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imagePath = openFileDialog.FileName;
                    SendImage(imagePath);
                    //MessagePopup(" ", Pengguna.Head);
                }
            }
            else
            {
                MessageBox.Show("No client connected. Start the server and wait for a client to connect.");
            }
        }

        private void AcceptClients()
        {
            try
            {
                while (true)
                {
                    client = server.AcceptTcpClient();

                    NetworkStream networkStream = client.GetStream();
                    reader = new StreamReader(networkStream);
                    writer = new StreamWriter(networkStream);
                    writer.AutoFlush = true;

                    isConnected = true;
                    label_Status.BackColor = Color.Green;
                    UpdateStatusLabel("CONNECTED");
                    DisplayMessage("Client connected.", Pengguna.body);

                    // Start listening for incoming messages from the client
                    Task.Run(() => ReceiveMessages());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error accepting client: " + ex.Message);
            }
        }

        private void ReceiveMessages()
        {
            try
            {
                while (true)
                {
                    string message = reader.ReadLine();

                    if (IsBase64Image(message))
                    {
                        // Received image
                        DisplayImageFromBase64(message);
                        //MessagePopup("", Pengguna.body);
                    }
                    else
                    {
                        // Received message
                        DisplayMessage("Client: " + message, Pengguna.body);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error receiving message: " + ex.Message);
            }
        }

        private bool IsBase64Image(string message)
        {
            // Check if the message is a valid base64 encoded image
            try
            {
                byte[] imageBytes = Convert.FromBase64String(message);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image.FromStream(ms);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void DisplayImageFromBase64(string base64Image)
        {
            pictureBox_Image.Invoke(new Action(() =>
            {
                try
                {
                    byte[] imageBytes = Convert.FromBase64String(base64Image);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        Image image = Image.FromStream(ms);

                        // Acquire lock to access the clipboard
                        lock (ClipboardLock)
                        {
                            Clipboard.SetImage(image);
                            richTextBox_Display.Paste();
                            richTextBox_Display.AppendText(Environment.NewLine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error displaying image: " + ex.Message);
                }
            }));
        }


        private void SendMessage(string message)
        {
            writer.WriteLine(message);
            DisplayMessage("You: " + message, Pengguna.Head);
        }

        private void SendImage(string imagePath)
        {
            if (isConnected)
            {
                try
                {
                    byte[] imageData = File.ReadAllBytes(imagePath);
                    string base64Image = Convert.ToBase64String(imageData);

                    // Acquire lock to access the clipboard
                    lock (ClipboardLock)
                    {
                        writer.WriteLine(base64Image);
                    }

                    MessagePopup("You uploaded an image", Pengguna.Head);
                    DisplayImage(imagePath);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error sending image: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("It is not connected to the server. Press the connect button to connect to the server");
            }
        }
        private void DisplayImage(string imagePath)
        {
            pictureBox_Image.Invoke(new Action(() =>
            {
                Image image = Image.FromFile(imagePath);
                Clipboard.SetImage(image);
                richTextBox_Display.Paste();
                richTextBox_Display.AppendText(Environment.NewLine);
            }));
            MessagePopup(" ", Pengguna.body);
        }

        private void DisplayMessage(string message, Pengguna user)
        {
            richTextBox_Display.Invoke(new Action(() =>
            {
                MessagePopup(message + Environment.NewLine, user);
                //richTextBox_Display.Focus();
                richTextBox_Display.ScrollToCaret();
            }));
            
        }

        private void UpdateStatusLabel(string status)
        {
            label_Status.Invoke(new Action(() =>
            {
                label_Status.Text = status;
            }));
        }
    }
}
