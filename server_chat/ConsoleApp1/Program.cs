using System;
using System.Net.Sockets;
using System.IO;
public class ServerSocket1
{
    public static void Main()
    {
        try
        {
            bool status = true;
            string serverid = "[주] ";
            string clientid = "[수] ";
            string servermessage = "";
            string clientmessage = "";
            TcpListener tcpListener = new TcpListener(9000);
            tcpListener.Start();
            Console.WriteLine("Wating Connection...");
            Socket socketForClient = tcpListener.AcceptSocket();
            Console.WriteLine("'수' 님이 127.0.0.1에서 접속하셨습니다.");
            NetworkStream networkStream = new NetworkStream(socketForClient);
            StreamWriter streamwriter = new StreamWriter(networkStream);
            StreamReader streamreader = new StreamReader(networkStream);


            while (status)
            {
                if (socketForClient.Connected)
                {
                    servermessage = streamreader.ReadLine();
                    Console.WriteLine(clientid + ": " + servermessage);
                    if (servermessage == "/q")
                    {
                        status = false;
                        streamreader.Close();
                        networkStream.Close();
                        streamwriter.Close();
                        return;
                    }
                    Console.Write(serverid + ": ");
                    clientmessage = Console.ReadLine();
                    streamwriter.WriteLine(clientmessage);
                    streamwriter.Flush();
                }
            }
            streamreader.Close();
            networkStream.Close();
            streamwriter.Close();
            socketForClient.Close();
            Console.WriteLine("나갔어");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}