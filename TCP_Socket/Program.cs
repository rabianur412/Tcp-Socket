using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCP_Socket
{
    class Program
    {
        const int ServerPortNum = 60509;
        public static void Server()
        {
            //Soket yarat
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, ServerPortNum);
            Socket welcomingSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            welcomingSocket.Bind(serverEndPoint);

            //Bağlantı için bekle
            welcomingSocket.Listen();
            Socket connectionSocket = welcomingSocket.Accept();//mesaj alışverişini sağlamak için connection soketi dönecek

            //Alınan mesajı göster
            byte[] buffer = new byte[1024];
            int numberOfBytesReceived = connectionSocket.Receive(buffer);
            byte[] receivedBytes = new byte[numberOfBytesReceived];
            Array.Copy(buffer, receivedBytes, numberOfBytesReceived);
            string receivedMessage = Encoding.Default.GetString(receivedBytes);
            Console.WriteLine(receivedMessage);

            //Alınan mesajı cliente gönder
            connectionSocket.Send(receivedBytes);
            Console.ReadLine();
        }
        public static void Client()
        {
            //Soket yarat
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Loopback, 0);
            Socket clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Bind(clientEndPoint);

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, ServerPortNum);
            clientSocket.Connect(serverEndPoint);

            string messageToSend = "Hello ben Rabia";
            byte[] bytestoSend = Encoding.Default.GetBytes(messageToSend);
            clientSocket.Send(bytestoSend);

            byte[] buffer = new byte[1024];
            int numberOfBytesReceived = clientSocket.Receive(buffer);
            byte[] receivedBytes = new byte[numberOfBytesReceived];
            Array.Copy(buffer, receivedBytes, numberOfBytesReceived);
            string receivedMessage = Encoding.Default.GetString(receivedBytes);
            Console.WriteLine(receivedMessage);
            
        }

        static void Main(string[] args)
        {
            string inp = Console.ReadLine();
            if(inp.Equals("s"))
            {
                Server();
            }
            else if(inp.Equals("c"))
            {
                Client();
            }
            else
            {
                Console.WriteLine("Yanlış harf girdiniz.");
            }
            Console.ReadLine();
        }
    }
}
