using System;

namespace TwitchLib.TwitchIRC
{
    public class IrcEventArgs : EventArgs
    {
        private readonly IrcMessageData _Data;
        
        public IrcMessageData Data {
            get {
                return _Data;
            }
        }
         
        internal IrcEventArgs(IrcMessageData data)
        {
            _Data = data;
        }
    }
}
