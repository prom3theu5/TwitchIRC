using System;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using TwitchLib.TwitchIRC;

namespace ConsoleApp1
{
    public class Program
    {
        //todo: Set These To Test
        private static string _oauth = "";
        private static string _user = "";
        private static string _autoJoinChannel = "";

        private static IrcConnection _client;
        private static Task _listenThread;

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.LiterateConsole()
                .MinimumLevel.Debug()
                .CreateLogger();
            Log.Information("Starting");

            _client = new IrcConnection();
            _client.AutoReconnect = true;
            _client.AutoRetry = true;
            _client.AutoRetryLimit = 0;
            _client.OnConnected += Connected;
            _client.OnReadLine += ReadLine;
            _client.OnDisconnected += Disconnected;
            _client.OnConnectionError += ConnectionError;
            _client.Connect("irc.chat.twitch.tv", 6667);
            bool runForever = true;

            while (runForever)
            {
                var input = Console.ReadLine();

                if (input.StartsWith("exit", StringComparison.CurrentCultureIgnoreCase))
                {
                    _client.Disconnect();
                    runForever = false;
                }

                if (input.StartsWith("send ", StringComparison.CurrentCultureIgnoreCase))
                {
                    SendMessage(input.Substring(4));
                }
            }
        }

        private static void SendMessage(string message)
        {
            string twitchMessage = $":{_user}!{_user}@{_user}" +
                $".tmi.twitch.tv PRIVMSG #{_autoJoinChannel} :{message}";
            _client.WriteLine(Encoding.GetEncoding(0).GetString(Encoding.UTF8.GetBytes(twitchMessage)));
        }

        private static void ConnectionError(object sender, EventArgs e)
        {
            Log.Error("Connection Error: {error}", e);
        }

        private static void Disconnected(object sender, EventArgs e)
        {
            Log.Debug("Disconnected from Twitch");
        }

        private static void ReadLine(object sender, ReadLineEventArgs e)
        {
            Log.Debug("Raw Line Received: {line}", e.Line);
        }

        private static void Connected(object sender, EventArgs e)
        {
            Log.Debug("Connected To Twitch. Logging In");

            _client.WriteLine(Rfc2812.Pass(_oauth), Priority.Critical);
            _client.WriteLine(Rfc2812.Nick(_user), Priority.Critical);
            _client.WriteLine(Rfc2812.User(_user, 0, _user), Priority.Critical);

            _client.WriteLine("CAP REQ twitch.tv/membership");
            _client.WriteLine("CAP REQ twitch.tv/commands");
            _client.WriteLine("CAP REQ twitch.tv/tags");

            _client.WriteLine(Rfc2812.Join($"#{_autoJoinChannel}"));

            _listenThread = Task.Factory.StartNew(() =>
                {
                    Log.Debug("Login Complete. Listening on joined channel: {channel}", _autoJoinChannel);
                    while (_client.IsConnected)
                    {
                        try
                        {
                            _client.Listen();
                        }
                        catch (Exception ex)
                        {
                            Log.Error("Exception in Client {error}", ex);
                            throw ex;
                        }
                    }
                });
        }
    }
}
