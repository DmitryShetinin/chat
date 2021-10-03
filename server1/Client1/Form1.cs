using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Media;

namespace Client1
{
    public partial class Form1 : Form
    {
        private const int port = 8888;
        private const string server1 = "127.0.0.1";
        private const int port1 = 9000;
        private const int port2 = 8080;
        TcpListener server = null;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
       



        public Form1()
        {
            InitializeComponent();
        }

   

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                server = new TcpListener(localAddr, port1);
                server.Start();
         
                TcpClient client = server.AcceptTcpClient();
                listBox1.Items.Add(textBox1.Text);
                while (true)
                {
                    NetworkStream stream = client.GetStream();

                    // сообщение для отправки клиенту
                    string response = textBox1.Text;
                    // преобразуем сообщение в массив байтов
                    byte[] data = Encoding.UTF8.GetBytes(response);

                    // отправка сообщения
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Отправлено сообщение: {0}", response);
                    // закрываем поток
                    stream.Close();
                    // закрываем подключение
                    client.Close();
                    server.Stop();
                    break;
                }
            }
            catch
            {
                server = new TcpListener(localAddr, port1);
                server.Start();

                TcpClient client = server.AcceptTcpClient();
                listBox1.Items.Add(textBox1.Text);
                while (true)
                {
                    NetworkStream stream = client.GetStream();

                    // сообщение для отправки клиенту
                    string response = textBox1.Text;
                    // преобразуем сообщение в массив байтов
                    byte[] data = Encoding.UTF8.GetBytes(response);

                    // отправка сообщения
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Отправлено сообщение: {0}", response);
                    // закрываем поток
                    stream.Close();
                    // закрываем подключение
                    client.Close();
                    server.Stop();
                    break;
                }
            }


        }



        private void Form1_Load(object sender, EventArgs e)
        {
            server = new TcpListener(localAddr, port2);
            server.Start();
            CheckForIllegalCrossThreadCalls = false;
            new Thread(() =>
            {
                while (true)
                {
                    try
                    {

                        TcpClient client = new TcpClient();
                        client.Connect(server1, port);

                        byte[] data = new byte[256];
                        StringBuilder response = new StringBuilder();
                        NetworkStream stream = client.GetStream();

                        do
                        {
                            int bytes = stream.Read(data, 0, data.Length);
                            response.Append(Encoding.UTF8.GetString(data, 0, bytes));
                        }
                        while (stream.DataAvailable); // пока данные есть в потоке
            
                        listBox1.Items.Add(response.ToString());
                        SoundPlayer sp = new SoundPlayer("zxc1.wav");
                        sp.Play();
                        // Закрываем потоки
                        stream.Close();
                        client.Close();
                        server.Stop();

                    }
                    catch
                    {
                        
                    }



                }


            }).Start();
            // myThread.Start(); // запускаем поток
        }
    }
}
