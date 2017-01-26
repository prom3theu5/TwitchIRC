using System.Collections;

namespace TwitchLib.TwitchIRC
{
    /// <summary>
    /// 
    /// </summary>
    /// <threadsafety static="true" instance="true" />
    public class NonRfcChannel : Channel
    {
        private Hashtable _Owners = Hashtable.Synchronized(new Hashtable());
        private Hashtable _ChannelAdmins = Hashtable.Synchronized(new Hashtable());
        private Hashtable _Halfops = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"> </param>
        internal NonRfcChannel(string name) : base(name)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        public Hashtable Owners {
            get {
                return (Hashtable) _Owners.Clone();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        internal Hashtable UnsafeOwners {
            get {
                return _Owners;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        public Hashtable ChannelAdmins {
            get {
                return (Hashtable) _ChannelAdmins.Clone();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        internal Hashtable UnsafeChannelAdmins {
            get {
                return _ChannelAdmins;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        public Hashtable Halfops {
            get {
                return (Hashtable) _Halfops.Clone();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value> </value>
        internal Hashtable UnsafeHalfops {
            get {
                return _Halfops;
            }
        }
    }
}
