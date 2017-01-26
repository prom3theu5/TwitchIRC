namespace TwitchLib.TwitchIRC
{
    /// <summary>
    ///
    /// </summary>
    /// <threadsafety static="true" instance="true" />
    public class NonRfcChannelUser : ChannelUser
    {
        public bool IsOwner { get; set; }
        public bool IsChannelAdmin { get; set; }
        public bool IsHalfop { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel"> </param>
        /// <param name="ircuser"> </param>
        internal NonRfcChannelUser(string channel, IrcUser ircuser) : base(channel, ircuser)
        {
        }
    }
}
