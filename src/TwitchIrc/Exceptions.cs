using System;

namespace TwitchLib.TwitchIRC
{
    [Serializable]
    public class TwitchIRCException : Exception
    {
        public TwitchIRCException() : base()
        {
        }
        
        public TwitchIRCException(string message) : base(message)
        {
        }
        
        public TwitchIRCException(string message, Exception e) : base(message, e)
        {
        }
    }

    [Serializable]
    public class ConnectionException : TwitchIRCException
    {
        public ConnectionException() : base()
        {
        }
        
        public ConnectionException(string message) : base(message)
        {
        }
        
        public ConnectionException(string message, Exception e) : base(message, e)
        {
        }
    }
    
    [Serializable]
    public class CouldNotConnectException : ConnectionException
    {
        public CouldNotConnectException() : base()
        {
        }
        
        public CouldNotConnectException(string message) : base(message)
        {
        }
        
        public CouldNotConnectException(string message, Exception e) : base(message, e)
        {
        }
    }
    
    [Serializable]
    public class NotConnectedException : ConnectionException
    {
        public NotConnectedException() : base()
        {
        }
        
        public NotConnectedException(string message) : base(message)
        {
        }
        
        public NotConnectedException(string message, Exception e) : base(message, e)
        {
        }
    }
    
    [Serializable]
    public class AlreadyConnectedException : ConnectionException
    {
        public AlreadyConnectedException() : base()
        {
        }
        
        public AlreadyConnectedException(string message) : base(message)
        {
        }
        
        public AlreadyConnectedException(string message, Exception e) : base(message, e)
        {
        }
    }
}
