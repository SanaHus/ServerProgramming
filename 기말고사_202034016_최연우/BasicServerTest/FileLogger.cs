using System;
using System.IO;
using System.Text.Json;

namespace FinalTestSample
{
    public class FileLogger
    {
        DateTime time = DateTime.Now;

        public string _fileName { get; set; }

        public FileLogger(string fileName)
        {
            //문제 6:
            //_fileName에 filename + 날짜.log 로 로그파일명 저장
            //예제 > fileName이 Log일때 Log220616.log

            _fileName = fileName + time.ToString("yyMMdd") + ".log";

        }

        public void Write(string logType, string logMessage)
        {
            //문제 7:
            //로그는 [날짜 시간] <logType> logMessage로 작성되도록 아래 log라는 string에 저장
            //예시 > [2022-06-16 12:00:00] <Error> 로그인 에러
            FileStream fs;
            StreamWriter sw;

            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string _log = "[" + time + "]" + " <" + logType + "> " + logMessage;
                     
            fs = new FileStream(_fileName, FileMode.Append, FileAccess.Write);            

            sw = new StreamWriter(fs);
            sw.WriteLine(_log);
            sw.Close();
        }

        

    }
}
