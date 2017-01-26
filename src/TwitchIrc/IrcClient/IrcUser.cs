using System.Collections.Specialized;

namespace TwitchLib.TwitchIRC
{
    /// <summary>
    /// This class manages the user information.
    /// </summary>
    /// <remarks>
    /// only used with channel sync
    /// <seealso cref="IrcClient.ActiveChannelSyncing">
    ///   IrcClient.ActiveChannelSyncing
    /// </seealso>
    /// </remarks>
    /// <threadsafety static="true" instance="true" />
    public class IrcUser
    {
        private IrcClient _IrcClient;
        private string    _Nick     = null;
        private string    _Ident    = null;
        private string    _Host     = null;
        private string    _Realname = null;
        private bool      _IsIrcOp  = false;
        private bool      _IsRegistered = false;
        private bool      _IsAway   = false;
        private string    _Server   = null;
        private int       _HopCount = -1;

        internal IrcUser(string nickname, IrcClient ircclient)
        {
            _IrcClient = ircclient;
            _Nick      = nickname;
        }

        /// <summary>
        /// Gets or sets the nickname of the user.
        /// </summary>
        /// <remarks>
        /// Do _not_ set this value, it will break channel sync!
        /// </remarks>
        public string Nick {
            get {
                return _Nick;
            }
            set {
                _Nick = value;
            }
        }

        /// <summary>
        /// Gets or sets the identity (username) of the user which is used by some IRC networks for authentication. 
        /// </summary>
        /// <remarks>
        /// Do _not_ set this value, it will break channel sync!
        /// </remarks>
        public string Ident {
            get {
                return _Ident;
            }
            set {
                _Ident = value;
            }
        }

        /// <summary>
        /// Gets or sets the hostname of the user. 
        /// </summary>
        /// <remarks>
        /// Do _not_ set this value, it will break channel sync!
        /// </remarks>
        public string Host {
            get {
                return _Host;
            }
            set {
                _Host = value;
            }
        }

        /// <summary>
        /// Gets or sets the supposed real name of the user.
        /// </summary>
        /// <remarks>
        /// Do _not_ set this value, it will break channel sync!
        /// </remarks>
        public string Realname {
            get {
                return _Realname;
            }
            set {
                _Realname = value;
            }
        }

        /// <summary>
        /// Gets or sets the server operator status of the user
        /// </summary>
        /// <remarks>
        /// Do _not_ set this value, it will break channel sync!
        /// </remarks>
        public bool IsIrcOp {
            get {
                return _IsIrcOp;
            }
            set {
                _IsIrcOp = value;
            }
        }

        /// <summary>
        /// Gets or sets the registered status of the user
        /// </summary>
        public bool IsRegistered {
            get {
                return _IsRegistered;
            }
            internal set {
                _IsRegistered = value;
            }
        }

        /// <summary>
        /// Gets or sets away status of the user
        /// </summary>
        /// <remarks>
        /// Do _not_ set this value, it will break channel sync!
        /// </remarks>
        public bool IsAway {
            get {
                return _IsAway;
            }
            set {
                _IsAway = value;
            }
        }

        /// <summary>
        /// Gets or sets the server the user is connected to
        /// </summary>
        /// <remarks>
        /// Do _not_ set this value, it will break channel sync!
        /// </remarks>
        public string Server {
            get {
                return _Server;
            }
            set {
                _Server = value;
            }
        }

        /// <summary>
        /// Gets or sets the count of hops between you and the user's server
        /// </summary>
        /// <remarks>
        /// Do _not_ set this value, it will break channel sync!
        /// </remarks>
        public int HopCount {
            get {
                return _HopCount;
            }
            set {
                _HopCount = value;
            }
        }

        /// <summary>
        /// Gets the list of channels the user has joined
        /// </summary>
        public string[] JoinedChannels {
            get {
                Channel          channel;
                string[]         result;
                string[]         channels       = _IrcClient.GetChannels();
                StringCollection joinedchannels = new StringCollection();
                foreach (string channelname in channels) {
                    channel = _IrcClient.GetChannel(channelname);
                    if (channel.UnsafeUsers.ContainsKey(_Nick)) {
                        joinedchannels.Add(channelname);
                    }
                }

                result = new string[joinedchannels.Count];
                joinedchannels.CopyTo(result, 0);
                return result;
                //return joinedchannels;
            }
        }
    }
}
