using System.Net;
using System.Threading;
using System.Text.Json;


namespace FinalTestSample
{
    class data
    {

    }

    public class Global
    {
        public static Mutex mut = new Mutex();    
        
        public static List<string> id = new List<string>();

        public static void InitServer()
        {
            //서버 초기화 로직을 여기에 담으세요
        }

        public static void ContextHandle(object httplistener)
        {
            mut.WaitOne();
            HttpListener listener = (HttpListener)httplistener;
            HttpListenerContext context = listener.GetContext();
            mut.ReleaseMutex();

            //문제 1 : 사용자의 입력을 받고 출력을 한다.
            //
            //
            //
            //---------------------------
            //여기부턴 예제입니다.

            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            response.AddHeader("Access-Control-Allow-Origin", "*");
            Stream body = request.InputStream;
            System.Text.Encoding encoding = request.ContentEncoding;
            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);

            string rawurl = request.RawUrl;
            string httpmethod = request.HttpMethod;

            string result = "";

            result += string.Format("httpmethod = {0}\r\n", httpmethod);
            result += string.Format("rawurl = {0}\r\n", rawurl);
            System.Console.WriteLine(result);


            Message a = new Message();
            string responseString = JsonSerializer.Serialize(a);
            System.Console.WriteLine(responseString);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();

            //----------------------------요기까지
        }

        public static int GetServerID()
        {
            //서버에서 고유 id를 부여해서 리턴하시오
            return 0;
            
        }

        /*
        static string GetNewMessage(string pName)
        {
            //사용자가 받지못한 최신 메시지를 출력하세요
        }

        static bool checkLogin(string pID , string serverID)
        {
            //사용자ID가 서버에 저장되어있는지 확인하세요.
            //저장되어있다면 true 아니라면 false
        } */
    }

    public class ServerLoop
    {
        HttpListener _listener;
        string _address;

        public ServerLoop(string address, string port)
        {
            RestartLoop();
            //문제2 :_address에 localhost 주소와 port가 입력된 주소를 작성하시오

        }

        public void RestartLoop()
        {
            _listener = new HttpListener();
        }

        

        public void AddRestAPI(string apiName)
        {
            //문제3 :address 뒤에 apiName이 추가된 prefix를 추가하세요

        }
        public void Run()
        {
            _listener.Start();
            Console.WriteLine("Listening...");


            while (true)
            {
                ///문제4: 응답이 들어올경우 mutex를 이용하여 아래 코드를 새로운 접속이 생길때마다 1개의 Thread가 생기도록 개선하시오
                //Thread t = new Thread(new ParameterizedThreadStart(Global.ContextHandle));
                // t.Start(_listener);

                



            }
        }

        public void Close()
        {
            _listener.Stop();
        }

       

        void EndLoop()
        {
            if (_listener != null)
            {
                _listener.Stop();
                _listener.Close();
            }
        }
    }



    public class Program
    {
        public static int Main()
        {
            FileLogger fLog = new FileLogger("Log");

            Global.InitServer();

            //Log type 종류
            //<Exception>
            //<Debug>
            //<Record>
            fLog.Write("Debug","I hate life.");

            ServerLoop server = new ServerLoop("http://127.0.0.1","3000");
            server.AddRestAPI("GodDamnChatService");
           
            server.Run();
            server.Close();


            return 0;
        }
    }
}