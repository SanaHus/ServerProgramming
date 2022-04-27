using System;
using System.Net.Sockets;
using System.IO;
public class ClientSocket1
{
    static void Main(string[] args)
    {
        TcpClient socketForServer;
        bool status = true;
        try
        {
            string ip = Console.ReadLine();
            if(ip != "/c 127.0.0.1:9000")
            {
                Console.WriteLine("다시 입력해~");
                ip = Console.ReadLine();
            }

            Console.WriteLine("127.0.0.1:9000에 접속시도중...");
            Console.WriteLine("'주'님께 연결되었습니다. ");
            socketForServer = new TcpClient("localhost", 9000);
        }
        catch
        {
            Console.WriteLine("Failed to Connect to server{0}:999", "localhost");
            return;
        }
        NetworkStream networkStream = socketForServer.GetStream();
        StreamReader streamreader = new StreamReader(networkStream);
        StreamWriter streamwriter = new StreamWriter(networkStream);

        try
        {
            string clientid = "[수] ";
            string serverid = "[주] ";
            string clientmessage = "";
            string servermessage = "";
            while (status)
            {
                if (serverid == null)
                {
                    serverid = streamreader.ReadLine();
                }
                Console.Write(clientid + ": ");
                clientmessage = Console.ReadLine();
                if ((clientmessage == "/q") || (clientmessage == "/Q"))
                {
                    status = false;
                    streamwriter.WriteLine("채팅 끝");
                    streamwriter.Flush();
                    streamreader.Close();
                    networkStream.Close();
                    streamwriter.Close();
                }
                if ((clientmessage != "/q") && (clientmessage != "/Q"))
                {
                    streamwriter.WriteLine(clientmessage);
                    streamwriter.Flush();
                    servermessage = streamreader.ReadLine();
                    Console.WriteLine(serverid + ": " + servermessage);
                }
            }
        }
        catch
        {
            Console.WriteLine("서버 꺼짐");
        }
        streamreader.Close();
        networkStream.Close();
        streamwriter.Close();
    }
}