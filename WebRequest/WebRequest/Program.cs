using System;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Threading;


namespace WebRequestandTrack
{

    public class MainClass
    {
        public const string GMAIL_SERVER = "smtp.live.com";
        //Connecting port
        public const int PORT = 587;

        public static void Main(string[] args)
        {
            while (true)
            {
                string strMsg = string.Empty;
                try
                {
                    WebRequest request = WebRequest.Create("http://www.jseea.cn/zkyw/zkyw_channel173_1.html");
                    WebResponse response = request.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));

                    strMsg = reader.ReadToEnd();

                    reader.Close();
                    reader.Dispose();
                    response.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                DateTime now = DateTime.Now;

                if (strMsg.Contains("08-06"))
                {

                    try
                    {
                        Console.WriteLine("找到啦--{0}", now);
                        SmtpClient mailServer = new SmtpClient(GMAIL_SERVER, PORT);
                        mailServer.EnableSsl = true;

                        //Provide your email id with your password.
                        //Enter the app-specfic password if two-step authentication is enabled.
                        mailServer.Credentials = new System.Net.NetworkCredential("sender@email.com", "password");

                        //Senders email.
                        string from = "sender@email.com";
                        //Receiver email
                        string to = "eceiver@email.com";

                        MailMessage msg = new MailMessage(from, to);

                        //Subject of the email.
                        msg.Subject = "Enter the subject here";

                        //Specify the body of the email here.
                        msg.Body = "The message goes here.";

                        mailServer.Send(msg);

                        Console.WriteLine("MAIL SENT. Press any key to exit...");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unable to send email. Error : " + ex);
                    }

                    Console.ReadKey();
                }
                else
                {

                    Console.WriteLine("没找到--{0}", now);
                    Thread.Sleep(60000);
                }



            }
        }
    }
}
