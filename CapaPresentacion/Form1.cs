using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const int PORT_NO = 5500;
        const string SERVER_IP = "10.10.10.66";
        private void button1_Click(object sender, EventArgs e)
        {
            //System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
            //TcpClient socketForServer;
            //try
            //{
            //    //clientSocket.Connect("10.10.10.66", 5500);
            //}
            //catch (Exception exc)
            //{

            //    throw;
            //}
            //try
            //{

            //    //---data to send to the server---
            //    string textToSend = "cerededererederedederedderee";// DateTime.Now.ToString();

            //    //---create a TCPClient object at the IP and port no.---
            //    TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
            //    NetworkStream nwStream = client.GetStream();
            //    byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

            //    //---send the text---
            //    Console.WriteLine("Sending : " + textToSend);
            //    nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            //    //---read back the text---
            //    byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            //    int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);

            //    MessageBox.Show("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));

            //    Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            //    Console.ReadLine();
            //    client.Close();
            //    //socketForServer = new TcpClient("10.10.10.66", 5500);
            //}
            //catch(Exception ex)
            //{
            //    throw ex;
            //    //Console.WriteLine(
            //    //"Failed to connect to server at {0}:999", "localhost");
            //    //return;
            //}

            //NetworkStream networkStream = socketForServer.GetStream();
            //System.IO.StreamReader streamReader =
            //new System.IO.StreamReader(networkStream);
            //System.IO.StreamWriter streamWriter =
            //new System.IO.StreamWriter(networkStream);
            //try
            //{
            //    string outputString;
            //    // read the data from the host and display it
            //    {
            //        outputString = streamReader.ReadLine();
            //        Console.WriteLine(outputString);
            //        streamWriter.WriteLine("Client Message");
            //        Console.WriteLine("Client Message");
            //        streamWriter.Flush();
            //    }
            //}
            //catch
            //{
            //    Console.WriteLine("Exception reading from Server");
            //}
            //networkStream.Close();

            string error = "";
            CapaModulo.Bll.Basico._ejecuta_proceso(ref error);
            //FeBata.
        }
    }
}
