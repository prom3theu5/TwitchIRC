using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace TwitchLib.TwitchIRC
{
    /// <summary>
    /// 
    /// </summary>
    public class Channel
    {
        private string           _Name;
        private string           _Key       = String.Empty;
        private Hashtable        _Users     = Hashtable.Synchronized(new Hashtable());
        private Hashtable        _Ops       = Hashtable.Synchronized(new Hashtable());
        private Hashtable        _Voices    = Hashtable.Synchronized(new Hashtable());
        private StringCollection _Bans      = new StringCollection();
        private List<string>     _BanExcepts = new List<string>();
        private List<string>     _InviteExcepts = new List<string>();
        private string           _Topic     = String.Empty;
        private int              _UserLimit;
        private string           _Mode      = String.Empty;
        private DateTime         _ActiveSyncStart;
        private DateTime         _ActiveSyncStop;
        private TimeSpan         _ActiveSyncTime;
        private bool             _IsSycned;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"> </param>
        internal Channel(string name)
        {
            _Name = name;
            _ActiveSyncStart = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        public string Name {
            get {
                return _Name;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        public string Key {
            get {
                return _Key;
            }
            set {
                _Key = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        public Hashtable Users {
            get {
                return (Hashtable)_Users.Clone();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        internal Hashtable UnsafeUsers {
            get {
                return _Users;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        public Hashtable Ops {
            get {
                return (Hashtable)_Ops.Clone();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        internal Hashtable UnsafeOps {
            get {
                return _Ops;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        public Hashtable Voices {
            get {
                return (Hashtable)_Voices.Clone();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        internal Hashtable UnsafeVoices {
            get {
                return _Voices;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        public StringCollection Bans {
            get {
                return _Bans;
            }
        }

        public List<string> BanExceptions {
            get {
                return _BanExcepts;
            }
        }

        public List<string> InviteExceptions {
            get {
                return _InviteExcepts;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        public string Topic {
            get {
                return _Topic;
            }
            set {
                _Topic = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        public int UserLimit {
            get {
                return _UserLimit;
            }
            set {
                _UserLimit = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        public string Mode {
            get {
                return _Mode;
            }
            set {
                _Mode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        public DateTime ActiveSyncStart {
            get {
                return _ActiveSyncStart;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        public DateTime ActiveSyncStop {
            get {
                return _ActiveSyncStop;
            }
            set {
                _ActiveSyncStop = value;
                _ActiveSyncTime = _ActiveSyncStop.Subtract(_ActiveSyncStart);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        public TimeSpan ActiveSyncTime {
            get {
                return _ActiveSyncTime;
            }
        }

        public bool IsSycned {
            get {
                return _IsSycned;
            }
            set {
                _IsSycned = value;
            }
        }
    }
}
